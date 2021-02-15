using Comos.Walkinside.Data;
using Comos.Walkinside.Common.Branches;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataSdkExamples
{
    /// <summary>
    /// Console application that injects FRT into specified project.
    /// Designed to work with example "Stabilizer" project.
    /// </summary>
    class App
    {
        class FrtBranchInfo
        {
            public string Level { get; set; }
            public string Purpose { get; set; }
            public IBranch SourceCadBranch { get; set; }
        }

        static void Main(string[] args)
        {
            string projectAddress;
            if (args.Length < 1)
            {
                Console.Write("Enter path of \"Stabilizer\" example project: ");
                projectAddress = Console.ReadLine().Trim();
            }
            else
            {
                projectAddress = args[0];
            }

            Console.WriteLine("Opening project \"{0}\"...", projectAddress);
            using (var project = ProjectManager.Open(projectAddress))
            {
                Console.WriteLine();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Branches in "Stabilizer" project have attributes coming
                // from PDMS, including such attributes as "Level" and "Purpose".
                //
                // The idea of this FRT generator is to:
                // - select all CAD branches that have "Level" attribute and
                //   that either have or inherit "Purpose" attribute;
                // - group them first by "Level", then by "Purpose"; and
                // - inject them into project's FRT hierarchy.
                //
                // The resulting FRT will look something like:
                // <root>
                //  +- Level A
                //     +- Purpose X
                //        +- branch 1
                //        +- branch 2
                //        +- ...
                //        +- branch N
                //     +- Purpose Y
                //        +- ...
                //  +- Level B
                //     +- Purpose X
                //        +- ...

                // Prepare attribute keys for "Level" and "Purpose".

                IProjectAttributeSource pdmsAttributeSource = project.AttributeSources.GetAll()
                    .Single(attrSrc => attrSrc.Kind.Equals(ProjectAttributeSourceKinds.Pdms));
                IAttributeKey levelKey = pdmsAttributeSource.CreateKey("Level");
                IAttributeKey purposeKey = pdmsAttributeSource.CreateKey("Purpose");

                // Select all CAD branches of the project.

                IEnumerable<IBranch> cadRootBranches = project.Branches.GetRoots(BranchKind.Cad);
                IEnumerable<IBranch> allCadBranches = cadRootBranches.Concat(
                    cadRootBranches.SelectMany(cadRoot => cadRoot.Descendants));

                // Scan all CAD branches and retrieve their "Level" and "Purpose".

                var cadBranchesWithLevelAndPurpose = allCadBranches
                    .Select(cadBranch => new
                    {
                        Level = cadBranch.Attributes.Get(levelKey),
                        Purpose = GetAttributeWithInheritance(cadBranch, purposeKey),
                        SourceCadBranch = cadBranch,
                    })
                    .Where(branchInfo => branchInfo.Level != null && branchInfo.Purpose != null);

                // Translate data into sequence of FrtBranchInfo and simplify
                // IAttributeInstances into simple strings.

                var frtBranchInfos = cadBranchesWithLevelAndPurpose
                    .Select(branchInfo => new FrtBranchInfo
                    {
                        Level = (string)branchInfo.Level.Value,
                        Purpose = (string)branchInfo.Purpose.Value,
                        SourceCadBranch = branchInfo.SourceCadBranch,
                    });

                // Sort branch info so that branches can be created sequentially.
                // The sequence will look something like:
                //
                // Level A | Purpose X | branch 1
                // Level A | Purpose X | ...
                // Level A | Purpose X | branch N1
                // Level A | Purpose Y | branch 1
                // Level A | Purpose Y | ...
                // Level A | Purpose Y | branch N2
                // Level B | Purpose X | branch 1
                // Level B | Purpose X | ...
                // Level B | Purpose X | branch N3
                // ...     | ...       | ...

                var orderedFrtBranchInfos = frtBranchInfos
                    .OrderBy(branchInfo => branchInfo.Level)
                    .ThenBy(branchInfo => branchInfo.Purpose)
                    .ThenBy(branchInfo => branchInfo.SourceCadBranch.Name);

                // Translate branch infos to creation params, to be fed into
                // Branch.Create().

                var branchCreationParams = GenerateBranchCreationParams(orderedFrtBranchInfos);

                // Fastest way to create many branches is with Branches.Create().
                // It has significant optimizations, especially for standalone
                // projects. Current maximum rate, if creating branches one-by-one,
                // is 6-10 branches per second. Using Branches.Create() it
                // is possible to create thousands of branches per second.

                Console.WriteLine("Injecting FRT branches...");
                project.Branches.Create(branchCreationParams);
                Console.WriteLine();

                stopwatch.Stop();
                Console.WriteLine("Done in {0}.", stopwatch.Elapsed);
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        /// <returns>
        /// Value of specified attribute that is set on the branch or its
        /// closest ancestor; null if neither branch nor any of its ancestors
        /// has this attribute set.
        /// </returns>
        static IAttributeInstance GetAttributeWithInheritance(
            IBranch branch,
            IAttributeKey attrKey)
        {
            if (branch == null)
            {
                return null;
            }

            var attr = branch.Attributes.Get(attrKey);

            if (attr != null)
            {
                return attr;
            }

            return GetAttributeWithInheritance(branch.Parent, attrKey);
        }

        /// <returns>
        /// Description of all the branches to be created, which should be fed
        /// into project.Branches.Create for fast bulk creation of entire
        /// branch hierarchy.
        /// </returns>
        static IEnumerable<BranchCreationParams> GenerateBranchCreationParams(
            IEnumerable<FrtBranchInfo> orderedBranchInfos)
        {
            var newFrtRootName = string.Format(
                "Created by {0} at {1:s}",
                Process.GetCurrentProcess().ProcessName,
                DateTime.Now);
            var newFrtRoot = new BranchCreationParams(BranchKind.Frt, newFrtRootName);
            Console.WriteLine("Root: {0}", newFrtRoot.Name);
            yield return newFrtRoot;

            BranchCreationParams currentLevel = null;
            BranchCreationParams currentPurpose = null;

            foreach (var branchInfo in orderedBranchInfos)
            {
                var levelDisplayName = string.Format("Level {0}", branchInfo.Level);
                if (currentLevel == null
                    || currentLevel.Name != levelDisplayName)
                {
                    // Another "Level", emit new "Level" branch as child of FRT root.
                    currentLevel = new BranchCreationParams(
                        name: levelDisplayName,
                        parentReference: newFrtRoot.ReferenceKey,
                        referenceKey: Guid.NewGuid());
                    Console.WriteLine("  {0}", currentLevel.Name);
                    yield return currentLevel;
                }

                var purposeDisplayName = branchInfo.Purpose;
                if (currentPurpose == null
                    || currentPurpose.Name != purposeDisplayName)
                {
                    // Another "Purpose", emit new "Purpose" branch as child of "Level".
                    currentPurpose = new BranchCreationParams(
                        name: purposeDisplayName,
                        parentReference: currentLevel.ReferenceKey,
                        referenceKey: Guid.NewGuid());
                    Console.WriteLine("    {0}", currentPurpose.Name);
                    yield return currentPurpose;
                }

                // Emit new FRT branch with name and geometry of corresponding
                // CAD branch, as child of "Purpose".
                var branch = new BranchCreationParams(
                    name: branchInfo.SourceCadBranch.Name,
                    parentReference: currentPurpose.ReferenceKey,
                    referenceKey: Guid.NewGuid(),
                    elementSources: new[] { branchInfo.SourceCadBranch });
                Console.WriteLine("      {0}", branch.Name);
                yield return branch;
            }
        }
    }
}

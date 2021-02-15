using Comos.Walkinside.Data;
using Comos.Walkinside.Common.Branches;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataSdkExamples
{
    /// <summary>
    /// Simple console application that opens specified project
    /// and prints some statistics about it.
    /// </summary>
    class App
    {
        const string None = "<none>";

        static void Main(string[] args)
        {
            string projectAddress;
            if (args.Length < 1)
            {
                Console.Write("Enter project path or address: ");
                projectAddress = Console.ReadLine().Trim();
            }
            else
            {
                projectAddress = args[0];
            }

            var creds = new ConsoleCredentialProvider();

            Console.WriteLine("Opening project \"{0}\"...", projectAddress);
            using (var project = ProjectManager.Open(projectAddress, creds))
            {
                Console.WriteLine();

                Console.WriteLine("Project basics:");
                //
                var isOnlineProject = project.AsOnlineProject() != null;
                var attachedProjectNames = project.Children.Select(p => p.Name);
                var projectDirPath = Path.GetDirectoryName(project.FilePath);
                var fileCount = 0;
                long totalSize = 0;
                foreach (var filePath in Directory.EnumerateFiles(
                                    projectDirPath, "*", SearchOption.AllDirectories))
                {
                    ++fileCount;
                    totalSize += new FileInfo(filePath).Length;
                }
                if (isOnlineProject && project.ArchivePath != null)
                {
                    // Count .tlz.
                    ++fileCount;
                    totalSize += new FileInfo(project.ArchivePath).Length;
                }
                //
                Console.WriteLine("  Path: {0}", project.FilePath);
                Console.WriteLine("  Name: {0}", project.Name);
                Console.WriteLine("  Disposition: {0}", isOnlineProject ? "online" : "standalone");
                Console.WriteLine("  Attached projects: {0}", FormatList(attachedProjectNames));
                Console.WriteLine("  Local files: {0}", fileCount);
                Console.WriteLine("  Local disk space occupied: {0:0.0} MB", totalSize / 1e6);
                Console.WriteLine();

                Console.WriteLine("Project-persisted settings:");
                //
                var settingCount = project.Settings.GetSnapshot().Count();
                var readOnlySettingCount = project.Settings.GetSnapshot().Count(s => !s.CanModify);
                //
                Console.WriteLine("  Settings: {0}", settingCount);
                Console.WriteLine("  Read-only settings: {0}", readOnlySettingCount);
                Console.WriteLine();

                Console.WriteLine("Branches:");
                //
                var haveCad = project.Branches.GetRoots(BranchKind.Cad).Any();
                var haveFrt = project.Branches.GetRoots(BranchKind.Frt).Any();
                var otherBranchHierarchiyKinds = project.Branches.GetRoots()
                    .Select(b => b.Kind)
                    .Where(k => k != BranchKind.Cad && k != BranchKind.Frt)
                    .Distinct()
                    .OrderBy(k => k)
                    .Select(k => string.Format("#{0}", k));
                //
                Console.WriteLine("  CAD hierarchy: {0}", FormatBool(haveCad));
                Console.WriteLine("  FRT hierarchy: {0}", FormatBool(haveFrt));
                Console.WriteLine("  Other kinds of branch hierarchies: {0}", FormatList(otherBranchHierarchiyKinds));
                Console.WriteLine();

                Console.WriteLine("CAD hierarchy:");
                ReportBranchHierarchy(project, BranchKind.Cad);
                Console.WriteLine();

                if (haveFrt)
                {
                    Console.WriteLine("FRT hierarchy:");
                    ReportBranchHierarchy(project, BranchKind.Frt);
                    Console.WriteLine();
                }

                Console.WriteLine("Attributes:");
                //
                var attributeSources = project.AttributeSources.GetAll()
                    .Select(attrSrc => attrSrc.Kind.Id);
                //
                Console.WriteLine("  Sources: {0}", FormatList(attributeSources));
                Console.WriteLine();

                Console.WriteLine("Design review objects:");
                //
                var viewpointCount = project.Viewpoints.GetAll().Count();
                var redlineCount = project.RedlineGroups.GetAll()
                    .SelectMany(rg => rg.Redlines.GetAll()) // SelectMany combines redlines from all groups into single flat sequence.
                    .Count();
                var uriTemplateCount = project.UriTemplates.GetAll().Count();
                //
                Console.WriteLine("  Viewpoints: {0}", viewpointCount);
                Console.WriteLine("  Redlines: {0}", redlineCount);
                Console.WriteLine("  URI templates: {0}", uriTemplateCount);
                Console.WriteLine();

                Console.WriteLine("  Scenarios: <unknown>");
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        static void ReportBranchHierarchy(IProject project, BranchKind kind)
        {
            var roots = project.Branches.GetRoots(kind).ToArray();
            var branchCount = 0;
            var hiddenBranchCount = 0;
            var maxDepth = 0;
            var minBranchNameLength = int.MaxValue;
            var maxBranchNameLength = int.MinValue;
            var haveNonAsciiNames = false;
            var seenBranchNames = new HashSet<string>();
            var haveDuplicatedNames = false;
            foreach (var b in roots.Concat(roots.SelectMany(root => root.Descendants)))
            {
                ++branchCount;
                if (!b.IsViewable)
                {
                    ++hiddenBranchCount;
                }
                maxDepth = Math.Max(maxDepth, b.DepthLevel);
                minBranchNameLength = Math.Min(minBranchNameLength, b.Name.Length);
                maxBranchNameLength = Math.Max(maxBranchNameLength, b.Name.Length);
                haveNonAsciiNames |= b.Name.Any(c => c < 0 || c > 127);
                haveDuplicatedNames |= !seenBranchNames.Add(b.Name);
            }

            Console.WriteLine("  Branches: {0}", branchCount);
            if (branchCount == 0)
            {
                return;
            }

            Console.WriteLine(
                "  Hidden branches: {0} ({1}%)",
                hiddenBranchCount, hiddenBranchCount * 100 / branchCount);
            Console.WriteLine("  Deepest branch level: {0}", maxDepth + 1);
            Console.WriteLine(
                "  Branch name length: {0}-{1} characters",
                minBranchNameLength, maxBranchNameLength);
            Console.WriteLine("  Non-ASCII branch names: {0}", FormatBool(haveNonAsciiNames));
            Console.WriteLine("  Duplicated branch names: {0}", FormatBool(haveDuplicatedNames));
        }

        static string FormatList(IEnumerable<string> items)
        {
            items = items.ToArray();
            return items.Any() ? string.Join(", ", items) : None;
        }

        static string FormatBool(bool value)
        {
            return value ? "yes" : "no";
        }
    }
}

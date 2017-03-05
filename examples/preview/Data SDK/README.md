Data SDK Examples
=================

## Using Example Projects

To use examples in this folder, install COMOS Walkinside 10.2 SP1 64-bit
or 32-bit in "Just Me" mode ("All Users" will not work), then open
`DataSdkExamplesFor64BitWalkinside.sln` or `DataSdkExamplesFor32BitWalkinside.sln`
accordingly in Visual Studio 2012 or higher.

Example projects are configured in such a way as to automatically reference
SDK assemblies from your Walkinside installation. Also, build output will be
written directly to Walkinside's binaries folder. That way you can compile
examples and run them from Walkinside binary folder, where all dependencies
can be found.


## List of Examples

### Example 1: Project Overview

A simple console application that opens specified project and prints some
statistics about it. Also demonstrates implementing a custom credential
provider.

### Example 2: FRT Generation

A console application that injects FRT into specified project, based on
engineering attributes. Designed to work with example "Stabilizer" project.


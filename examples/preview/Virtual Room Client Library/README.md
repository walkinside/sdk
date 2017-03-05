Virtual Room Client Examples
============================

## Using Example Projects

To use examples in this folder, install COMOS Walkinside 10.2 SP1 64-bit
or 32-bit in "Just Me" mode ("All Users" will not work), then open 
`VirtualRoomClientLibraryExamplesFor64BitWalkinside.sln` or 
`VirtualRoomClientLibraryExamplesFor32BitWalkinside.sln` accordingly 
in Visual Studio 2012 or higher.

Example projects are configured in such a way as to automatically reference
SDK assemblies from your Walkinside installation. Also, build output will be
written directly to Walkinside's binaries folder. That way you can compile
examples and run them from Walkinside binary folder, where all dependencies
can be found.


## List of Examples

### Example 1: Flocking

A simple console application that connects to a Virtual Room and spawns a number
of bots. Bots will follow the first peer that looks like human, in circle
formation.


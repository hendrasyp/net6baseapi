### Nuget Packages Dependecies ###
1. **Serilog**
2. **Fast Member**, In .NET reflection is slow... well, kinda slow. If you need access to the members of an arbitrary type, with the type and member-names known only at runtime - then it is frankly hard (especially for DLR types). This library makes such access easy and fast.
3. **LZStringCSharp**, LZ String C# Compression Library
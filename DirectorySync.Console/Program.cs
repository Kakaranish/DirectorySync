using DirectorySync.Console;
using DirectorySync.Console.Types;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.RegisterStartupDependencies();
var serviceProvider = serviceCollection.BuildServiceProvider();

var fsMetadataProvider = serviceProvider.GetRequiredService<IFilesystemMetadataProvider>();

var fs1Dir = DirectoryPath.From("./test_data/pics_fs");
var fs1Metadata = fsMetadataProvider.GetFor(fs1Dir);

var fs2Dir = DirectoryPath.From("./test_data/pics_fs2");
var fs2Metadata = fsMetadataProvider.GetFor(fs2Dir);

var fsMetadataComparator = serviceProvider.GetRequiredService<FilesystemMetadataComparator>();
fsMetadataComparator.Compare(fs1Metadata, fs2Metadata);

Console.ReadKey();


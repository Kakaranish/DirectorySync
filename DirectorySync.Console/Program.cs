using DirectorySync.Console.DependencyInjection;
using DirectorySync.Console.Services;
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

var fsChangeDetector = serviceProvider.GetRequiredService<IFilesystemChangeDetector>();
var fsChanges = fsChangeDetector.GetChanges(fs1Metadata, fs2Metadata).ToList();

Console.ReadKey();


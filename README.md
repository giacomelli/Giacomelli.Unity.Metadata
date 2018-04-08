# Giacomelli.Unity.Metadata

[![Build status](https://ci.appveyor.com/api/projects/status/hp51ath4ijyjf38v?svg=true)](https://ci.appveyor.com/project/giacomelli/Giacomelli.Unity.Metadata)
[![License](http://img.shields.io/:license-MIT-blue.svg)](https://raw.githubusercontent.com/giacomelli/Giacomelli.Unity.Metadata/master/LICENSE)
[![Coverage Status](https://coveralls.io/repos/giacomelli/Giacomelli.Unity.Metadata/badge.svg?branch=master&service=github)](https://coveralls.io/github/giacomelli/Giacomelli.Unity.Metadata?branch=master)
[![FxCop](http://badgessharp.apphb.com/badges/giacomelli/Giacomelli.Unity.Metadata/FxCop)](https://ci.appveyor.com/project/giacomelli/Giacomelli.Unity.Metadata/build/artifacts)
[![DupFinder](http://badgessharp.apphb.com/badges/giacomelli/Giacomelli.Unity.Metadata/DupFinder)](https://ci.appveyor.com/project/giacomelli/Giacomelli.Unity.Metadata/build/artifacts)

Library to read metadata from Unity3d asset files when "Asset Serialization" mode is "Force text".

--------

## Code quality
- Tested on **Unity 5.3.6**
- 100% unit test code coverage.
- FxCop validated.
- Code duplicated verification.
- Good (and well used) design patterns.  

--------


## Usage

### Initialize
Just call the line below to initilize all default services.

```csharp

MetadataBootstrap.Setup();

```

### Get scripts metadata
```csharp

var log = MetadataBootstrap.Log;
var service = MetadataBootstrap.ScriptMetadataService;
var scriptsMetadata = service.GetScripts();

foreach(var sm in scriptsMetadata)
{
	Log.Debug("FileId: {0}", sm.FileId);
	Log.Debug("FullName: {0}", sm.FullName);
	Log.Debug("Name: {0}", sm.Name);
	Log.Debug("Guid: {0}", sm.Guid);
}

```

### Get prefabs metadata
```csharp

var log = MetadataBootstrap.Log;
var service = MetadataBootstrap.PrefabMetadataService;
var prefabsMetadata = service.GetPrefabs();

foreach(var pm in prefasMetadata)
{
	Log.Debug("Name: {0}", pm.Name);
	Log.Debug("Path: {0}", pm.Path);
	Log.Debug("MonoBehaviours count: {0}", pm.MonoBehaviours.Count());
	Log.Debug("Materials count: {0}", pm.Materials.Count());
}

```

### Fix "Missing (Mono Script)"
```csharp

var prefabService = MetadataBootstrap.PrefabMetadataService;
var prefabs = prefabService.GetPrefabs();
var typeService = MetadataBootstrap.TypeService;
var assetRepository = MetadataBootstrap.AssetRepository;

foreach (var prefab in prefabs)
{
    var missingMonoBehaviours = prefab.GetMissingMonoBehaviours(assetRepository, typeService);

    prefabService.FixMissingMonobehaviours(prefab, missingMonoBehaviours);
}

```

## FAQ

Having troubles? 

- Ask on Twitter [@ogiacomelli](http://twitter.com/ogiacomelli).
- Ask on [Stack Overflow](http://stackoverflow.com/search?q=Giacomelli.Unity.Metadata). 
 
 --------

## How to improve it?

Create a fork of [Giacomelli.Unity.Metadata](https://github.com/giacomelli/Giacomelli.Unity.Metadata/fork). 

Did you change it? [Submit a pull request](https://github.com/giacomelli/Giacomelli.Unity.Metadata/pull/new/master).


## License
Licensed under the The MIT License (MIT).
In others words, you can use this library for developement any kind of software: open source, commercial, proprietary and alien.

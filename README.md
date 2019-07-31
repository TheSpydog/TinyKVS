# TinyKVS

A tiny, dependency-free C# wrapper for iCloud Key Value Storage. Only confirmed to work on iOS, but it should work on macOS and tvOS too.

**Usage:**

At the start of your app:
```cs
TinyKVS.Initialize();
TinyKVS.Synchronize();
```

At the start of every update tick of your app:
```cs
TinyKVS.Update();
```

To detect external changes to KVS data:
```cs
void KVSExternalChangeCallback(TinyKVS.ChangeReason changeReason, string[] keysChanged)
{
	// Do something...
}

// When your app starts...
TinyKVS.OnExternalChange += ExternalChangeCallback;
```

To fetch information from KVS:
```cs
bool myBool = TinyKVS.GetBool("example_key_1");
byte[] myData = TinyKVS.GetData("example_key_2");
double myDouble = TinyKVS.GetDouble("example_key_3");
long mylong = TinyKVS.GetLong("example_key_4");
string myString = TinyKVS.GetString("example_key_5");
```

To set information in KVS:
```cs
TinyKVS.SetBool("bool_key", myBool);
TinyKVS.SetData("data_key", myData);
// and so on...
```

To print all key-value pairs stored in KVS:
```cs
foreach (var pair in TinyKVS.GetKeyValuePairs())
{
	Console.WriteLine(pair.Key + ": " + pair.Value);
}
```

To get the estimated size of the key-value store:
```cs
ulong kvsSize = GetEstimatedKVSSizeInBytes();
```
(This will not be exact because it also takes into consideration the size of some NSObjects used for containing the data.)

To remove information from KVS:
```cs
TinyKVS.RemoveKeyValuePair("key_to_remove");

// Or if you want to clear all the data...
TinyKVS.RemoveAllKeyValuePairs();
```
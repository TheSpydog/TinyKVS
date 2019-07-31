/* TinyKVS - A tiny, dependency-free C# wrapper for iCloud Key-Value Storage.
 *
 * Written by Caleb Cornett.
 * Released under the Unlicense (see LICENSE.txt for details).
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TinyKVS
{
    public static class TinyKVS
    {
        #region Public KVS Change Reason Enum

        public enum ChangeReason
        {
            ServerChange = 0,
            InitialSyncChange = 1,
            QuotaViolationChange = 2,
            AccountChange = 3
        }

        #endregion

        #region Private Objective-C Interop

        const string nativeLibrary = "/usr/lib/libobjc.A.dylib";

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern void objc_msgSend(IntPtr receiver, IntPtr selector);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern void objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr ptr, ulong val);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern void objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr ptr);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern void objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr ptr1, IntPtr ptr2, IntPtr ptr3, IntPtr ptr4);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern void objc_msgSend(IntPtr receiver, IntPtr selector, byte val, IntPtr ptr);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern void objc_msgSend(IntPtr receiver, IntPtr selector, double val, IntPtr ptr);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern void objc_msgSend(IntPtr receiver, IntPtr selector, long val, IntPtr ptr);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern void objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr ptr1, IntPtr ptr2);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern IntPtr intptr_objc_msgSend(IntPtr receiver, IntPtr selector);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern IntPtr intptr_objc_msgSend(IntPtr receiver, IntPtr selector, string str);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern IntPtr intptr_objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr ptr);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern IntPtr intptr_objc_msgSend(IntPtr receiver, IntPtr selector, ulong val);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern IntPtr intptr_objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr ptr, ulong val);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern IntPtr intptr_objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr ptr1, ulong val1, ulong val2, IntPtr ptr2);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern byte byte_objc_msgSend(IntPtr receiver, IntPtr selector);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern byte byte_objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr ptr);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern double double_objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr str);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern long long_objc_msgSend(IntPtr receiver, IntPtr selector);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern long long_objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr ptr);

        [DllImport(nativeLibrary, EntryPoint = "objc_msgSend")]
        private static extern ulong ulong_objc_msgSend(IntPtr receiver, IntPtr selector);

        [DllImport(nativeLibrary)]
        private static extern IntPtr sel_registerName(string name);

        [DllImport(nativeLibrary)]
        private static extern IntPtr objc_getClass(string name);

        [DllImport(nativeLibrary)]
        private static extern byte class_addMethod(IntPtr _class, IntPtr sel, IntPtr imp, string types);

        // Selectors

        private static IntPtr selAlloc = sel_registerName("alloc");
        private static IntPtr selInitWithUTF8String = sel_registerName("initWithUTF8String:");
        private static IntPtr selUTF8String = sel_registerName("UTF8String");
        private static IntPtr selDefaultStore = sel_registerName("defaultStore");
        private static IntPtr selSynchronize = sel_registerName("synchronize");
        private static IntPtr selBoolForKey = sel_registerName("boolForKey:");
        private static IntPtr selDataForKey = sel_registerName("dataForKey:");
        private static IntPtr selLength = sel_registerName("length");
        private static IntPtr selGetBytes = sel_registerName("getBytes:length:");
        private static IntPtr selDoubleForKey = sel_registerName("doubleForKey:");
        private static IntPtr selLongLongForKey = sel_registerName("longLongForKey:");
        private static IntPtr selStringForKey = sel_registerName("stringForKey:");
        private static IntPtr selRemoveObjectForKey = sel_registerName("removeObjectForKey:");
        private static IntPtr selDictionaryRepresentation = sel_registerName("dictionaryRepresentation");
        private static IntPtr selAllKeys = sel_registerName("allKeys");
        private static IntPtr selCount = sel_registerName("count");
        private static IntPtr selObjectAtIndex = sel_registerName("objectAtIndex:");
        private static IntPtr selUserInfo = sel_registerName("userInfo");
        private static IntPtr selValueForKey = sel_registerName("valueForKey:");
        private static IntPtr selDefaultCenter = sel_registerName("defaultCenter");
        private static IntPtr selAddObserver = sel_registerName("addObserver:selector:name:object:");
        private static IntPtr selSetBoolForKey = sel_registerName("setBool:forKey:");
        private static IntPtr selSetDataForKey = sel_registerName("setData:forKey:");
        private static IntPtr selSetDoubleForKey = sel_registerName("setDouble:forKey:");
        private static IntPtr selSetLongLongForKey = sel_registerName("setLongLong:forKey:");
        private static IntPtr selSetStringForKey = sel_registerName("setString:forKey:");
        private static IntPtr selDataWithPropertyList = sel_registerName("dataWithPropertyList:format:options:error:");
        private static IntPtr selDataWithBytes = sel_registerName("dataWithBytes:length:");
        private static IntPtr selRelease = sel_registerName("release");
        private static IntPtr selDescription = sel_registerName("description");
        private static IntPtr selLongLongValue = sel_registerName("longLongValue");

        // Classes

        private static IntPtr classNSString = objc_getClass("NSString");
        private static IntPtr classNSUbiquitousKeyValueStore = objc_getClass("NSUbiquitousKeyValueStore");
        private static IntPtr classNSNotificationCenter = objc_getClass("NSNotificationCenter");
        private static IntPtr classNSPropertyListSerialization = objc_getClass("NSPropertyListSerialization");
        private static IntPtr classNSData = objc_getClass("NSData");

        #endregion

        #region Private Utilities

        private static Queue<IntPtr> toRelease = new Queue<IntPtr>();
        private static bool storeDidChange = false;
        private static ChangeReason changeReason;
        private static string[] keysChanged;

        private static IntPtr UTF8ToNSString(string str)
        {
            // Handle edge case where string is actually null
            if (str == null)
                return IntPtr.Zero;

            IntPtr newstr = intptr_objc_msgSend(classNSString, selAlloc);
            toRelease.Enqueue(newstr);

            return intptr_objc_msgSend(
                newstr,
                selInitWithUTF8String,
                str
            );
        }

        private static string NSStringToUTF8(IntPtr nsstr)
        {
            IntPtr nativeStr = intptr_objc_msgSend(
                nsstr,
                selUTF8String
            );

            return Marshal.PtrToStringAuto(nativeStr);
        }

        [ObjCRuntime.MonoPInvokeCallback(typeof(Action<IntPtr, IntPtr, IntPtr>))]
        private static void StoreDidChangeHandler(IntPtr self, IntPtr _cmd, IntPtr notification)
        {
            storeDidChange = true;
            IntPtr userInfo = intptr_objc_msgSend(notification, selUserInfo);

            // Get change reason
            IntPtr changeReasonNSNumber = intptr_objc_msgSend(
                userInfo,
                selValueForKey,
                UTF8ToNSString("NSUbiquitousKeyValueStoreChangeReasonKey")
            );
            changeReason = (ChangeReason)long_objc_msgSend(
                changeReasonNSNumber,
                selLongLongValue
            );

            // Get changed keys
            IntPtr nsarr = intptr_objc_msgSend(
                userInfo,
                selValueForKey,
                UTF8ToNSString("NSUbiquitousKeyValueStoreChangedKeysKey")
            );
            ulong arrlen = ulong_objc_msgSend(nsarr, selCount);
            keysChanged = new string[arrlen];
            for (ulong i = 0; i < arrlen; i += 1)
            {
                IntPtr nsstr = intptr_objc_msgSend(
                    nsarr,
                    selObjectAtIndex,
                    i
                );

                keysChanged[i] = NSStringToUTF8(nsstr);
            }
        }

        #endregion

        #region Private Store Reference

        private static IntPtr defaultStore;

        #endregion

        #region Public External Change Event

        public delegate void ExternalChangeHandler(ChangeReason reason, string[] keysChanged);
        public static event ExternalChangeHandler OnExternalChange;

        #endregion

        #region Public Utility Methods

        /* Call this method once at the start of your app. */
        public static void Initialize()
        {
            // Get a reference to the defaultStore for future calls.
            defaultStore = intptr_objc_msgSend(classNSUbiquitousKeyValueStore, selDefaultStore);
            if (defaultStore == IntPtr.Zero)
                throw new Exception("Could not get default store!");

            // Register for external change notifications
            IntPtr selStoreDidChange = sel_registerName("storeDidChange:");
            byte addedMethod = class_addMethod(
                classNSUbiquitousKeyValueStore,
                selStoreDidChange,
                Marshal.GetFunctionPointerForDelegate<Action<IntPtr, IntPtr, IntPtr>>(StoreDidChangeHandler),
                "v@:@"
            );
            if (addedMethod == 0)
                throw new Exception("Could not add method to defaultStore!");

            IntPtr defaultCenter = intptr_objc_msgSend(
                classNSNotificationCenter,
                selDefaultCenter
            );
            objc_msgSend(
                defaultCenter,
                selAddObserver,
                defaultStore,
                selStoreDidChange,
                UTF8ToNSString("NSUbiquitousKeyValueStoreDidChangeExternallyNotification"),
                defaultStore
            );
        }

        /* Call this method once at the start of your app, after Initialize(). */
        public static bool Synchronize()
        {
            byte val = byte_objc_msgSend(defaultStore, selSynchronize);
            return (val != 0);
        }

        /* Call this method at the start of your Update loop.
		 *
		 * The storeDidChange value has to be checked synchronously
         * because the native callback happens on another thread,
         * which can lead to unexpected freezes and all kinds of "fun".
		 */
        public static void Update()
        {
            if (storeDidChange)
            {
                if (OnExternalChange != null)
                    OnExternalChange((ChangeReason)changeReason, keysChanged);

                storeDidChange = false;
                keysChanged = null;
            }

            // Clean up our mess of allocated NSStrings...
            while (toRelease.Count > 0)
            {
                IntPtr obj = toRelease.Dequeue();
                objc_msgSend(obj, selRelease);
            }
        }

        /* Deletes the key-value pair with the given key. */
        public static void RemoveKeyValuePair(string key)
        {
            objc_msgSend(
                defaultStore,
                selRemoveObjectForKey,
                UTF8ToNSString(key)
            );
        }

        /* Removes every key-value pair from KVS. */
        public static void RemoveAllKeyValuePairs()
        {
            foreach (var pair in GetKeyValuePairs())
            {
                RemoveKeyValuePair(pair.Key);
            }
        }

        /* Returns a dictionary with all key-value pairs stored in KVS. */
        public static Dictionary<string, string> GetKeyValuePairs()
        {
            IntPtr dict = intptr_objc_msgSend(
                defaultStore,
                selDictionaryRepresentation
            );

            IntPtr arr = intptr_objc_msgSend(
                dict,
                selAllKeys
            );

            ulong count = ulong_objc_msgSend(
                arr,
                selCount
            );

            Dictionary<string, string> pairs = new Dictionary<string, string>();
            for (ulong i = 0; i < count; i++)
            {
                IntPtr key = intptr_objc_msgSend(
                    arr,
                    selObjectAtIndex,
                    i
                );

                IntPtr val = intptr_objc_msgSend(
                    dict,
                    selValueForKey,
                    key
                );

                pairs.Add(
                    NSStringToUTF8(key),
                    NSStringToUTF8(intptr_objc_msgSend(val, selDescription))
                );
            }

            return pairs;
        }

        /* Returns the estimated size of the KVS.
         * This is not exact because it takes into consideration
         * the size of some NSObjects used to contain the data.
         */
        public static ulong GetEstimatedKVSSizeInBytes()
        {
            IntPtr dict = intptr_objc_msgSend(
                defaultStore,
                selDictionaryRepresentation
            );

            IntPtr dictAsData = intptr_objc_msgSend(
                classNSPropertyListSerialization,
                selDataWithPropertyList,
                dict,
                200, // NSPropertyListBinaryFormat_v1_0
                0,
                IntPtr.Zero
            );

            return ulong_objc_msgSend(dictAsData, selLength);
        }

        #endregion

        #region Public Getters

        public static bool GetBool(string key)
        {
            byte val = byte_objc_msgSend(
                defaultStore,
                selBoolForKey,
                UTF8ToNSString(key)
            );

            return (val != 0);
        }

        public static byte[] GetData(string key)
        {
            IntPtr nsdata = intptr_objc_msgSend(
                defaultStore,
                selDataForKey,
                UTF8ToNSString(key)
            );

            ulong length = ulong_objc_msgSend(
                nsdata,
                selLength
            );

            byte[] data = new byte[length];

            objc_msgSend(
                nsdata,
                selGetBytes,
                Marshal.UnsafeAddrOfPinnedArrayElement(data, 0),
                length
            );

            return data;
        }

        public static double GetDouble(string key)
        {
            return double_objc_msgSend(
                defaultStore,
                selDoubleForKey,
                UTF8ToNSString(key)
            );
        }

        public static long GetLong(string key)
        {
            return long_objc_msgSend(
                defaultStore,
                selLongLongForKey,
                UTF8ToNSString(key)
            );
        }

        public static string GetString(string key)
        {
            IntPtr nsstr = intptr_objc_msgSend(
                defaultStore,
                selStringForKey,
                UTF8ToNSString(key)
            );

            return NSStringToUTF8(nsstr);
        }

        #endregion

        #region Public Setters

        public static void SetBool(string key, bool val)
        {
            objc_msgSend(
                defaultStore,
                selSetBoolForKey,
                (byte)(val ? 1 : 0),
                UTF8ToNSString(key)
            );
        }

        public static void SetData(string key, byte[] val)
        {
            IntPtr nsdata = intptr_objc_msgSend(
                classNSData,
                selDataWithBytes,
                Marshal.UnsafeAddrOfPinnedArrayElement(val, 0),
                (ulong)val.LongLength
            );

            objc_msgSend(
                defaultStore,
                selSetDataForKey,
                nsdata,
                UTF8ToNSString(key)
            );
        }

        public static void SetDouble(string key, double val)
        {
            objc_msgSend(
                defaultStore,
                selSetDoubleForKey,
                val,
                UTF8ToNSString(key)
            );
        }

        public static void SetLong(string key, long val)
        {
            objc_msgSend(
                defaultStore,
                selSetLongLongForKey,
                val,
                UTF8ToNSString(key)
            );
        }

        public static void SetString(string key, string val)
        {
            objc_msgSend(
                defaultStore,
                selSetStringForKey,
                UTF8ToNSString(val),
                UTF8ToNSString(key)
            );
        }

        #endregion
    }
}

#region ObjCRuntime Dummy Namespace

// This is a dummy namespace needed for Xamarin iOS/tvOS AOT compilation
namespace ObjCRuntime
{
    [AttributeUsage(AttributeTargets.Method)]
    class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute(Type t)
        {

        }
    }
}

#endregion
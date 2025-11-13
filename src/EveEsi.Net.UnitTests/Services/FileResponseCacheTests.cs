// using EveEsi.Net.Services;
//
// namespace EveEsi.Net.UnitTest.Services;
//
//
// [TestFixture]
//     public class FileResponseCacheTests
//     {
//         private string _tempFolder = null!;
//
//         [SetUp]
//         public void SetUp()
//         {
//             _tempFolder = Path.Combine(Path.GetTempPath(), "FileEsiCacheServiceTests", Guid.NewGuid().ToString("N"));
//             Directory.CreateDirectory(_tempFolder);
//         }
//
//         [TearDown]
//         public void TearDown()
//         {
//             try
//             {
//                 if (Directory.Exists(_tempFolder))
//                     Directory.Delete(_tempFolder, recursive: true);
//             }
//             catch
//             {
//                 // best effort cleanup
//             }
//         }
//
//         [Test]
//         public async Task StoreAndGet_ReturnsStoredValue()
//         {
//             // arrange
//             using var svc = new FileResponseCache(_tempFolder);
//             var key = "test-key-basic";
//             var value = new TestDto { Id = 42, Name = "Answer" };
//
//             // act
//             var stored = await svc.StoreValue(key, value);
//             var read = await svc.GetValue<TestDto>(key);
//
//             // assert
//             Assert.IsTrue(stored, "StoreValue should return true");
//             Assert.IsNotNull(read, "GetValue should return the stored object");
//             Assert.AreEqual(value.Id, read!.Id);
//             Assert.AreEqual(value.Name, read.Name);
//         }
//
//         [Test]
//         public async Task ExpiredItem_IsDeletedAndReturnsNull()
//         {
//             // arrange
//             using var svc = new FileResponseCache(_tempFolder);
//             var key = "test-key-expiry";
//             var value = "hello-expire";
//
//             // store with very short TTL
//             var ttl = TimeSpan.FromMilliseconds(300);
//             var stored = await svc.StoreValue(key, value, ttl);
//             Assert.IsTrue(stored);
//
//             // ensure file exists immediately after storing
//             var filePath = ComputeCachePath(_tempFolder, key);
//             Assert.IsTrue(File.Exists(filePath), "Cache file should exist immediately after storing");
//
//             // wait for TTL to pass (give a buffer)
//             await Task.Delay(TimeSpan.FromMilliseconds(600));
//
//             // act - read after expiry
//             var read = await svc.GetValue<string>(key);
//
//             // assert - should be null (default) and file should be removed
//             Assert.IsNull(read, "Expired item should return null");
//             Assert.IsFalse(File.Exists(filePath), "Expired cache file should be deleted");
//         }
//
//         [Test]
//         public async Task ConcurrentStoreAndGet_NoExceptionsAndValuesAreOneOfProduced()
//         {
//             // arrange
//             using var svc = new FileResponseCache(_tempFolder);
//             const int keyCount = 30;
//             const int opsPerKey = 50;
//             var rand = new Random(123);
//
//             var keys = Enumerable.Range(0, keyCount).Select(i => $"concurrent-key-{i}").ToArray();
//
//             // For each key, we'll run many parallel store operations that write unique integer values.
//             var tasks = keys.SelectMany(key =>
//             {
//                 return Enumerable.Range(0, opsPerKey).Select(async n =>
//                 {
//                     var value = (key, n); // tuple so it's unique per op
//                     await svc.StoreValue(key, value).ConfigureAwait(false);
//
//                     // occasionally read
//                     if (rand.NextDouble() < 0.3)
//                     {
//                         var read = await svc.GetValue<(string, int)>(key).ConfigureAwait(false);
//                         // read can be null if not yet written or expired — that's fine
//                     }
//                 });
//             }).ToArray();
//
//             // act
//             await Task.WhenAll(tasks);
//
//             // assert - after all operations, each key should have a stored value that matches the tuple shape and the "key" part should match
//             foreach (var key in keys)
//             {
//                 var read = await svc.GetValue<(string, int)>(key);
//                 Assert.IsNotNull(read, $"Expected a value for key '{key}' after concurrent stores.");
//                 Assert.AreEqual(key, read!.Item1, $"Stored tuple's key part should match for '{key}'");
//                 Assert.IsTrue(read.Item2 >= 0 && read.Item2 < opsPerKey, $"Stored tuple index {read.Item2} is out of expected range for '{key}'");
//             }
//         }
//
//         // Helper DTO used in tests
//         private sealed class TestDto
//         {
//             public int Id { get; set; }
//             public string? Name { get; set; }
//         }
//
//         // The service uses SHA256(hex) filenames; replicate that logic to find the file in tests.
//         private static string ComputeCachePath(string folder, string key)
//         {
//             using var sha = SHA256.Create();
//             var bytes = Encoding.UTF8.GetBytes(key);
//             var hash = sha.ComputeHash(bytes);
//             var hex = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
//             return Path.Combine(folder, $"{hex}.cachejson");
//         }
//     }




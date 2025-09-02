using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace _Scripts.GDrive
{
    public static class GetAssetFromDrive
    {
        private static readonly string ZIP_FILE_URL = "GOOGLE_DRIVE_DOWNLOAD_URL";

        private static readonly string ZipFilePath = Path.Combine(Application.dataPath, "folder.zip");
        private static readonly string ExtractPath = Path.Combine(Application.dataPath, "_Art");

        // for test
        [MenuItem("Tools/Download Assets")]
        public static async void StartDownload()
        {
            Debug.Log("Download Start");
            await DownloadAndExtractFolder();
        }

        private static async Task DownloadAndExtractFolder()
        {
            try
            {
                byte[] zipBytes = await FetchDataAsync(ZIP_FILE_URL);

                if (zipBytes == null || zipBytes.Length == 0)
                {
                    Debug.LogError("failed to download.");
                    return;
                }

                Debug.Log($"success download. file size: {zipBytes.Length} byte.");

                // Save zip file
                await File.WriteAllBytesAsync(ZipFilePath, zipBytes);

                Debug.Log("saved zip file. opening zip file");

                ExtractZipFile(ZipFilePath);
            }
            catch (Exception e)
            {
                Debug.LogError($"failed to download or open: {e.Message}");
            }
        }

        private static async Task<byte[]> FetchDataAsync(string url)
        {
            using var client = new HttpClient();
            try
            {
                return await client.GetByteArrayAsync(url);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"failed to get data: {e.Message}");
                return null;
            }
        }
        
        private static void ExtractZipFile(string zipPath)
        {
            if (Directory.Exists(ExtractPath))
            {
                foreach (var file in Directory.GetFiles(ExtractPath))
                    File.Delete(file);
                foreach (var dir in Directory.GetDirectories(ExtractPath))
                    Directory.Delete(dir, true);
            }
            else
            {
                Directory.CreateDirectory(ExtractPath);
            }

            try
            {
                ZipFile.ExtractToDirectory(zipPath, ExtractPath);
                Debug.Log("success to open zip file");
            }
            catch (IOException ex)
            {
                Debug.LogError("failed to open zip file: " + ex.Message);
            }
            finally
            {
                File.Delete(zipPath);
            }
        }
        
    }
}
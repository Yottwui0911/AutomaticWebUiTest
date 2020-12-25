using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumTest
{
  /// <summary>
  /// Selenium WebDriver を使った画面テストに共通する機能を提供します。
  /// </summary>
  [TestClass]
  public abstract class IisProcess
  {
    /// <summary>
    /// IIS のポート
    /// </summary>
    private const int Port = 2020;

    /// <summary>
    /// ベースアドレス
    /// </summary>
    protected static readonly string BaseAddress = $"http://localhost:{Port}";

    /// <summary>
    /// アプリケーションの配置位置
    /// </summary>
    private static string appPath = @"C:\deploy";

    /// <summary>
    /// IIS Express のプロセス
    /// </summary>
    private static Process _iisProcess;

    /// <summary>
    /// IIS Express を開始します。
    /// </summary>
    private static void StartIIS()
    {
      if (_iisProcess == null)
      {
        var applicationPath = GetApplicationPath(appPath);
        var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        var iisPath = Path.Combine(programFiles, "IIS Express", "iisexpress.exe");
        var startInfo = new ProcessStartInfo
        {
          FileName = iisPath,
          Arguments = $"/path:\"{applicationPath}\" /port:{Port}",
        };
        _iisProcess = Process.Start(startInfo);
      }
    }

    /// <summary>
    /// ソリューションフォルダ直下にあるテスト対象アプリのフォルダパスを取得します。
    /// </summary>
    /// <returns>テスト対象アプリのフォルダパス</returns>
    private static string GetApplicationPath(string applicationName)
    {
      var solutionFolder = Path.GetDirectoryName(
          Path.GetDirectoryName(
              Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
      return Path.Combine(solutionFolder, applicationName);
    }

    /// <summary>
    /// IIS Express を停止します。
    /// </summary>
    private static void StopIIS()
    {
      if (_iisProcess != null &&
          _iisProcess.HasExited == false)
      {
        _iisProcess.Kill();
      }
    }

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext testContext)
    {
      StartIIS();
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
      StopIIS();
    }
  }
}

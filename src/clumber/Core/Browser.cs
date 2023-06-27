// using System;
// using System.Threading.Tasks;
// using Microsoft.Playwright;

// namespace Clumber.Core;
// public class Browser : IDisposable, IAsyncDisposable
// {
//     public IBrowser PlBrowser { get; private set; }

//     public IPage? CurrentPage { get; private set; }

//     public IEnumerable<Entities.Identifier> Identifiers { get; private set; }

//     public Browser(IBrowser browser, IEnumerable<Entities.Identifier> identifiers)
//     {
//         PlBrowser = browser;
//         Identifiers = identifiers;
//     }

//     public void Dispose()
//     {
//         Dispose(true);
//         GC.SuppressFinalize(this);
//     }

//     public virtual void Dispose(bool disposing)
//     {
//         if (disposing)
//         {
//             /*var disposeTask = Task.Run(async () =>
//             {
//                 await PlBrowser.CloseAsync();
//                 await PlBrowser.DisposeAsync();
//             });
//             disposeTask.RunSynchronously();*/


//             _ = Task.FromResult(result: async () =>
//             {
//                 Console.WriteLine("Disposing");
//                 await ClosePage();
//                 foreach (var context in PlBrowser.Contexts)
//                 {
//                     await context.CloseAsync();
//                 }
//                 await PlBrowser.CloseAsync();
//                 return PlBrowser.DisposeAsync();
//             });

//         }
//     }

//     public static async Task<Browser> CreateChromeBrowser(IEnumerable<Entities.Identifier> identifiers)
//     {
//         var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

//         var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
//         {
//             Headless = true
//         });

//         return new Browser(browser, identifiers);
//     }

//     public async Task CreatePage()
//     {
//         if (CurrentPage is null)
//         {
//             CurrentPage = await PlBrowser.NewPageAsync();
//         }
//     }

//     public async Task ClosePage()
//     {
//         if (CurrentPage is not null)
//         {
//             await CurrentPage.CloseAsync();
//             CurrentPage = null;
//         }
//     }

//     public async ValueTask DisposeAsync()
//     {
//         Console.WriteLine("Disposing");
//         await ClosePage();
//         foreach (var context in PlBrowser.Contexts) {
//             await context.CloseAsync();
//         }
//         await PlBrowser.CloseAsync();
//         await PlBrowser.DisposeAsync();
//     }
// }

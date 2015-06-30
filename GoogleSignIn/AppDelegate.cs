using Foundation;
using UIKit;
using Google.iOS;
using System;

namespace GoogleSignIn
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register ("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate, IUISplitViewControllerDelegate, IGIDSignInDelegate
    {
        // class-level declarations

        public override UIWindow Window {
            get;
            set;
        }

        public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
        {

            // Override point for customization after application launch.
            var splitViewController = (UISplitViewController)Window.RootViewController;
            var navigationController = (UINavigationController)splitViewController.ViewControllers [1];
            navigationController.TopViewController.NavigationItem.LeftBarButtonItem = splitViewController.DisplayModeButtonItem;
            splitViewController.WeakDelegate = this;

            var googleInfo = NSDictionary.FromFile("GoogleService-Info.plist");
            GIDSignIn.SharedInstance.ClientID = googleInfo[new NSString("CLIENT_ID")].ToString();

            return true;
        }

        public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return GIDSignIn.SharedInstance.HandleURL (url, sourceApplication, annotation);
        }

        public override void OnResignActivation (UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground (UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground (UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated (UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate (UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        [Export ("splitViewController:collapseSecondaryViewController:ontoPrimaryViewController:")]
        public bool CollapseSecondViewController (UISplitViewController splitViewController, UIViewController secondaryViewController, UIViewController primaryViewController)
        {
            if (secondaryViewController.GetType () == typeof(UINavigationController) &&
                ((UINavigationController)secondaryViewController).TopViewController.GetType () == typeof(DetailViewController) &&
                ((DetailViewController)((UINavigationController)secondaryViewController).TopViewController).DetailItem == null) {
                // Return YES to indicate that we have handled the collapse by doing nothing; the secondary controller will be discarded.
                return true;
            }
            return false;
        }

        public void DidSignInForUser (GIDSignIn signIn, GIDGoogleUser user, NSError error)
        {
            Console.WriteLine ("AppDelegate:DidSignInForUser: {0} {1}", signIn, user);
            Dumper (user);
        }

        public static void Dumper (GIDGoogleUser user)
        {
            if (null == user) {
                Console.WriteLine ("user is null");
                return;
            }
            Console.WriteLine ("user.AccessibleScopes,Length: {0}", user.AccessibleScopes.Length);
            Console.WriteLine ("user.Authentication: {0}", user.Authentication);
            Console.WriteLine ("user.HostedDomain: {0}", user.HostedDomain);
            Console.WriteLine ("user.Profile: {0}", user.Profile);
            Console.WriteLine ("user.ServerAuthCode: {0}", user.ServerAuthCode);
            Console.WriteLine ("user.UserId: {0}", user.UserId);
            var profile = user.Profile;
            if (null == profile) {
                Console.WriteLine ("user.Profile is null");
            } else {
                Console.WriteLine ("profile.Email: {0}", profile.Email);
                Console.WriteLine ("profile.HasImage: {0}", profile.HasImage);
                Console.WriteLine ("profile.ImageURL {0}", profile.ImageURL (20));
                Console.WriteLine ("profile.Name: {0}", profile.Name);
            }
            var auth = user.Authentication;
            if (null == auth) {
                Console.WriteLine ("user.Authentication is null");
            } else {
                Console.WriteLine ("auth.AccessToken: {0}", auth.AccessToken);
                Console.WriteLine ("auth.AccessTokenExpirationDate: {0}", auth.AccessTokenExpirationDate);
                Console.WriteLine ("auth.IdToken: {0}", auth.IdToken);
                Console.WriteLine ("auth.RefreshToken: {0}", auth.RefreshToken);
            }
        }

    }
}



using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace Google.iOS
{
    
    // For more information, see http://developer.xamarin.com/guides/ios/advanced_topics/binding_objective-c/

    [BaseType (typeof(NSObject))]
    interface GGLContext
    {
        // - (void)configureWithError:(NSError **)error;
        [Export ("configureWithError:")]
        void ConfigureWithError (ref NSError error);

        // Get the shared instance of the GGLContext.
        // + (instancetype)sharedInstance;
        [Static, Export ("sharedInstance")]
        GGLContext SharedInstance { get; }
    }

    [BaseType (typeof(NSObject), Name = "GIDGoogleUser")]
    interface GIDGoogleUser
    {
        // The Google user ID.
        [Export ("userID")]
        string UserId { get; }

        // Representation of the Basic profile data. It is only available if |shouldFetchBasicProfile|
        // is set and either |signInWithUser| or |SignIn| has been completed successfully.
        // @property(nonatomic, readonly) GIDProfileData *profile;
        [Export ("profile")]
        GIDProfileData Profile { get; }

        // The authentication object for the user.
        // @property(nonatomic, readonly) GIDAuthentication *authentication;
        [Export ("authentication")]
        GIDAuthentication Authentication { get; }

        // The API scopes requested by the app in an array of |NSString|s.
        // @property(nonatomic, readonly) NSArray *accessibleScopes;
        [Export ("accessibleScopes")]
        string[] AccessibleScopes { get; }

        // For Google Apps hosted accounts, the domain of the user.
        // @property(nonatomic, readonly) NSString *hostedDomain;
        [Export ("hostedDomain")]
        string HostedDomain { get; }

        // An OAuth2 authorization code for the home server.
        // @property(nonatomic, readonly) NSString *serverAuthCode;
        [Export ("serverAuthCode")]
        string ServerAuthCode { get; }
    }

    // This class represents the basic profile information of a GIDGoogleUser.
    // @interface GIDProfileData : NSObject <NSCoding>
    [BaseType (typeof(NSObject), Name = "GIDProfileData")]
    interface GIDProfileData
    {
        // The Google user's email.
        // @property(nonatomic, readonly) NSString *email;
        [Export ("email")]
        string Email { get; }

        // The Google user's name.
        // @property(nonatomic, readonly) NSString *name;
        [Export ("name")]
        string Name { get; }

        // Whether or not the user has profile image.
        // @property(nonatomic, readonly) BOOL hasImage;
        [Export ("hasImage")]
        bool HasImage { get; }

        // Gets the user's profile image URL for the given dimension in pixels for each side of the square.
        // - (NSURL *)imageURLWithDimension:(NSUInteger)dimension;
        [Export ("imageURLWithDimension:")]
        NSUrl ImageURL (nuint dimension);
    }

    // This class represents the OAuth 2.0 entities needed for sign-in.
    //
    // @interface GIDAuthentication : NSObject <NSCoding>
    [BaseType (typeof(NSObject), Name = "GIDAuthentication")]
    [Protocol]
    interface GIDAuthentication
    {
        // The OAuth2 access token to access Google services.
        // @property(nonatomic, readonly) NSString *accessToken;
        [Export ("accessToken")]
        string AccessToken { get; }

        // The estimated expiration date of the access token.
        // @property(nonatomic, readonly) NSDate *accessTokenExpirationDate;
        [Export ("accessTokenExpirationDate")]
        NSDate AccessTokenExpirationDate { get; }

        // The OAuth2 refresh token to exchange for new access tokens.
        // @property(nonatomic, readonly) NSString *refreshToken;
        [Export ("refreshToken")]
        string RefreshToken { get; }

        // A JSON Web Token identifying the user. Send this token to your server to authenticate the user on
        // the server. For more information on JWTs, see
        // https://developers.google.com/accounts/docs/OAuth2Login#obtainuserinfo
        // @property(nonatomic, readonly) NSString *idToken;
        [Export ("idToken")]
        string IdToken { get; }
    }

    [BaseType (typeof(NSObject))]
    [Protocol]
    [Model]
    interface GIDSignInDelegate
    {
        // The sign-in flow has finished and was successful if |error| is |nil|.
        // - (void)signIn:(GIDSignIn *)signIn didSignInForUser:(GIDGoogleUser *)user withError:(NSError *)error;
        [Abstract]
        [Export ("signIn:didSignInForUser:withError:")]
        void DidSignInForUser (GIDSignIn signIn, GIDGoogleUser user, NSError error);

        // @optional

        // Finished disconnecting |user| from the app successfully if |error| is |nil|.
        // - (void)signIn:(GIDSignIn *)signIn didDisconnectWithUser:(GIDGoogleUser *)user withError:(NSError *)error;
        [Export ("signIn:didDisconnectWithUser:withError:")]
        void DidDisconnectWithUser (GIDSignIn signIn, GIDGoogleUser user, NSError error);
    }

    interface IGIDSignInDelegate
    {

    }

    [BaseType (typeof(NSObject))]
    [Protocol]
    [Model]
    interface GIDSignInUIDelegate
    {
        // The sign-in flow has finished selecting how to proceed, and the UI should no longer display
        // a spinner or other "please wait" element.
        // - (void)signInWillDispatch:(GIDSignIn *)signIn error:(NSError *)error;
        [Export ("signInWillDispatch:error:")]
        void SignInWillDispatch (GIDSignIn signIn, NSError error);

        // If implemented, this method will be invoked when sign in needs to display a view controller.
        // The view controller should be displayed modally (via UIViewController's |presentViewController|
        // method, and not pushed unto a navigation controller's stack.
        // - (void)signIn:(GIDSignIn *)signIn presentViewController:(UIViewController *)viewController;
        [Export ("signIn:presentViewController:")]
        void PresentViewController (GIDSignIn signIn, UIViewController viewController);

        // If implemented, this method will be invoked when sign in needs to dismiss a view controller.
        // Typically, this should be implemented by calling |dismissViewController| on the passed
        // view controller.
        // - (void)signIn:(GIDSignIn *)signIn dismissViewController:(UIViewController *)viewController;
        [Export ("signIn:dismissViewController:")]
        void DismissViewController (GIDSignIn signIn, UIViewController viewController);
    }

    interface IGIDSignInUIDelegate
    {

    }

    [BaseType (typeof(UIControl), Name = "GIDSignInButton")]
    interface GIDSignInButton
    {

        [Export ("setStyle:")]
        void SetStyle (GIDSignInButtonStyle style);

        [Export ("setColorScheme:")]
        void SetColorScheme (GIDSignInButtonColorScheme colorScheme);
    }

    // @interface GIDSignIn : NSObject
    [BaseType (typeof(NSObject), Name = "GIDSignIn")]
    interface GIDSignIn
    {
        // The authentication object for the current user, or |nil| if there is currently no logged in user.
        // @property(nonatomic, readonly) GIDGoogleUser *currentUser;
        [Export ("currentUser")]
        GIDGoogleUser CurrentUser { get; }

        // The object to be notified when authentication is finished.
        // @property(nonatomic, weak) id<GIDSignInDelegate> delegate;
        [Export ("delegate")]
        IGIDSignInDelegate Delegate { get; set; }

        // The object to be notified when sign in dispatch selection is finished.
        // @property(nonatomic, weak) id<GIDSignInUIDelegate> uiDelegate;
        [Export ("uiDelegate")]
        IGIDSignInUIDelegate UIDelegate { get; set; }

        //handleURL:sourceApplication:annotation:
        // This method should be called from your |UIApplicationDelegate|'s
        // |application:openURL:sourceApplication:annotation|.  Returns |YES| if |GIDSignIn| handled this
        // URL.
        // - (BOOL)handleURL:(NSURL *)url
        // sourceApplication:(NSString *)sourceApplication
        // annotation:(id)annotation;
        [Export ("handleURL:sourceApplication:annotation:")]
        bool HandleURL (NSUrl url, string sourceApplication, [NullAllowed] NSObject annotation);

        // Returns a shared |GIDSignIn| instance.
        // + (GIDSignIn *)sharedInstance;
        [Static]
        [Export ("sharedInstance")]
        GIDSignIn SharedInstance { get; }

        // Attempts to sign in a previously authenticated user without interaction.  The delegate will be
        // called at the end of this process indicating success or failure.
        // - (void)signInSilently;
        [Export ("signInSilently")]
        void SignInSilently ();

        // Starts the sign-in process.  The delegate will be called at the end of this process.  Note that
        // this method should not be called when the app is starting up, (e.g in
        // application:didFinishLaunchingWithOptions:). Instead use the |signInSilently| method.
        // - (void)signIn;
        [Export ("signIn")]
        void SignIn ();

        // Marks current user as being in the signed out state.
        // - (void)signOut;
        [Export ("signOut")]
        void SignOut ();

        // Disconnects the current user from the app and revokes previous authentication. If the operation
        // succeeds, the OAuth 2.0 token is also removed from keychain.
        // - (void)disconnect;
        [Export ("disconnect")]
        void Disconnect ();

        // The client ID of the app from the Google APIs console. Must set for sign-in to work.
        // - (NSString*) clientID
        [Export ("clientID")]
        string ClientID { get; }


        // The API scopes requested by the app in an array of NSStrings.
        // The default value is @[].
        // This property is optional. If you set it, set it before calling signIn.
        // - (NSArray*) scopes
        [Export ("scopes")]
        string[] Scopes { get; set; }

    }

}


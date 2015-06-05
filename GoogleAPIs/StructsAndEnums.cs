using System;

namespace Google.iOS
{
    // The various layout styles supported by the GIDSignInButton.
    // The minmum size of the button depends on the language used for text.
    // The following dimensions (in points) fit for all languages:
    // kGIDSignInButtonStyleStandard: 224 x 44
    // kGIDSignInButtonStyleWide:     306 x 44
    // kGIDSignInButtonStyleIconOnly:  44 x 44 (no text, fixed size)
    public enum GIDSignInButtonStyle
    {
        Standard = 0,
        Wide = 1,
        IconOnly = 2
    }

    // The various color schemes supported by the GIDSignInButton.
    public enum GIDSignInButtonColorScheme
    {
        Dark = 0,
        Light = 1
    }

    public enum GIDSignInErrorCode
    {
        // Indicates an unknown error has occured.
        Unknown = -1,
        // Indicates a problem reading or writing to the application keychain.
        Keychain = -2,
        // Indicates no appropriate applications are installed on the user's device which can handle
        // sign-in. This code will only ever be returned if switching to Safari has been disabled.
        NoSignInHandlersInstalled = -3,
        // Indicates there are no auth tokens in the keychain. This error code will be returned by
        // signInSilently if the user has never signed in before with the given scopes, or if they have
        // since signed out.
        HasNoAuthInKeychain = -4,
        // Indicates the user canceled the sign in request.
        CodeCanceled = -5,
    }
}


namespace SimplWindowsCWSIntegration;
        // class declarations
         class SimplPlusStringEventArgs;
         class CwsRoomAutomation;
         class SimplPlusUshortEventArgs;
     class SimplPlusStringEventArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING Value[];

        // class properties
    };

     class CwsRoomAutomation 
    {
        // class delegates

        // class events
        EventHandler PowerRequest ( CwsRoomAutomation sender, SimplPlusUshortEventArgs e );
        EventHandler SystemResetRequest ( CwsRoomAutomation sender, EventArgs e );
        EventHandler VideoMuteRequest ( CwsRoomAutomation sender, SimplPlusUshortEventArgs e );
        EventHandler AudioMuteRequest ( CwsRoomAutomation sender, SimplPlusUshortEventArgs e );
        EventHandler VolumeRequest ( CwsRoomAutomation sender, SimplPlusUshortEventArgs e );
        EventHandler VolumeLevelRequest ( CwsRoomAutomation sender, SimplPlusUshortEventArgs e );
        EventHandler LightsRequest ( CwsRoomAutomation sender, SimplPlusUshortEventArgs e );
        EventHandler LightingLevelRequest ( CwsRoomAutomation sender, SimplPlusUshortEventArgs e );
        EventHandler LightingSceneRequest ( CwsRoomAutomation sender, SimplPlusUshortEventArgs e );
        EventHandler ShadesRequest ( CwsRoomAutomation sender, SimplPlusUshortEventArgs e );
        EventHandler SourceRequest ( CwsRoomAutomation sender, SimplPlusStringEventArgs e );
        EventHandler CustomCommandRequest ( CwsRoomAutomation sender, SimplPlusStringEventArgs e );

        // class functions
        FUNCTION Start ( STRING path );
        FUNCTION Stop ();
        FUNCTION SetPower ( INTEGER power );
        FUNCTION SetVideoMute ( INTEGER mute );
        FUNCTION SetAudioMute ( INTEGER mute );
        FUNCTION SetVolume ( INTEGER level );
        FUNCTION SetLights ( INTEGER state );
        FUNCTION SetLightingLevel ( INTEGER level );
        FUNCTION SetLightingScene ( INTEGER scene );
        FUNCTION SetShades ( INTEGER state );
        FUNCTION SetSource ( STRING source );
        FUNCTION SetStatus ( STRING message );
        FUNCTION LogError ( STRING error );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class SimplPlusUshortEventArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER Value;

        // class properties
    };

namespace SimplWindowsCWSIntegration.Converters;
        // class declarations
         class JsonShadesBooleanConverter;
         class JsonMuteBooleanConverter;
         class JsonOnOffBooleanConverter;
         class JsonBooleanConverter;
         class DateFormatConverter;
     class JsonShadesBooleanConverter 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class JsonMuteBooleanConverter 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class JsonOnOffBooleanConverter 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class JsonBooleanConverter 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };


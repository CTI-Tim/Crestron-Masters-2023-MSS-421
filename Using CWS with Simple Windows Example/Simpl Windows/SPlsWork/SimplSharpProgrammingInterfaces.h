namespace Crestron.SimplSharp.Python;
        // class declarations
         class SimplSharpPythonInterface;
         class eModuleInstanceState;
         class PythonDataReceivedEventArgs;
         class PythonModuleInstance;
    static class SimplSharpPythonInterface 
    {
        // class delegates

        // class events

        // class functions
        static STRING_FUNCTION GetData ( STRING uniqueIdentifier );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

    static class eModuleInstanceState // enum
    {
        static SIGNED_LONG_INTEGER Unknown;
        static SIGNED_LONG_INTEGER Running;
        static SIGNED_LONG_INTEGER Stopped;
        static SIGNED_LONG_INTEGER Failed;
    };

     class PythonDataReceivedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Data[];
    };

     class PythonModuleInstance 
    {
        // class delegates

        // class events
        EventHandler DataReceived ( PythonModuleInstance sender, PythonDataReceivedEventArgs e );

        // class functions
        STRING_FUNCTION GetData ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING UniqueIdentifier[];
        STRING Arguments[][];
        STRING File[];
        eModuleInstanceState State;
        STRING StateInformation[];
    };


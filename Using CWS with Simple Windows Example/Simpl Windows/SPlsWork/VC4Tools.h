namespace VC4Tools;
        // class declarations
         class VC4Debugger;
         class Severity;
         class VC4DebugEventArgs;
    static class VC4Debugger 
    {
        // class delegates

        // class events
        static EventHandler ErrorMessage ( VC4DebugEventArgs e );

        // class functions
        static FUNCTION ProcessCommand ( STRING command );
        static FUNCTION Debug ( STRING message );
        static FUNCTION Notice ( STRING message );
        static FUNCTION Warning ( STRING message );
        static FUNCTION Error ( STRING message );
        static FUNCTION Exception ( STRING message );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

    static class Severity // enum
    {
        static SIGNED_LONG_INTEGER Debug;
        static SIGNED_LONG_INTEGER Notice;
        static SIGNED_LONG_INTEGER Warning;
        static SIGNED_LONG_INTEGER Error;
        static SIGNED_LONG_INTEGER Exception;
    };

     class VC4DebugEventArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING Message[];
        INTEGER Severity;

        // class properties
    };


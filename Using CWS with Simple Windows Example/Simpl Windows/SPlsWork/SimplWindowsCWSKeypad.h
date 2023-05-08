namespace SimplWindowsCWSKeypad;
        // class declarations
         class SimplPlusButtonEventArgs;
         class CwsKeypadExample;
     class SimplPlusButtonEventArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER Index;

        // class properties
    };

     class CwsKeypadExample 
    {
        // class delegates

        // class events
        EventHandler ButtonPressed ( CwsKeypadExample sender, SimplPlusButtonEventArgs e );

        // class functions
        FUNCTION Initialise ( STRING path );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };


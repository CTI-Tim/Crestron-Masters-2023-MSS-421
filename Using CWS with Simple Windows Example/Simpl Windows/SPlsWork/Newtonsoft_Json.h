namespace Newtonsoft.Json;
        // class declarations
         class ConstructorHandling;
         class DateFormatHandling;
         class DateParseHandling;
         class DateTimeZoneHandling;
         class DefaultJsonNameTable;
         class DefaultValueHandling;
         class FloatFormatHandling;
         class FloatParseHandling;
         class Formatting;
         class JsonArrayAttribute;
         class JsonConstructorAttribute;
         class JsonContainerAttribute;
         class JsonConvert;
         class JsonConverter;
         class JsonConverterAttribute;
         class JsonConverterCollection;
         class JsonDictionaryAttribute;
         class JsonException;
         class JsonExtensionDataAttribute;
         class JsonIgnoreAttribute;
         class JsonNameTable;
         class JsonObjectAttribute;
         class JsonPropertyAttribute;
         class JsonReader;
         class JsonReaderException;
         class JsonRequiredAttribute;
         class JsonSerializationException;
         class JsonSerializer;
         class JsonSerializerSettings;
         class JsonTextReader;
         class JsonTextWriter;
         class JsonToken;
         class JsonValidatingReader;
         class JsonWriter;
         class JsonWriterException;
         class MemberSerialization;
         class MetadataPropertyHandling;
         class MissingMemberHandling;
         class NullValueHandling;
         class ObjectCreationHandling;
         class PreserveReferencesHandling;
         class ReferenceLoopHandling;
         class Required;
         class StringEscapeHandling;
         class TypeNameAssemblyFormatHandling;
         class TypeNameHandling;
         class WriteState;
           class ErrorEventArgs;
           class ValidationEventArgs;
           class JsonSchema;
    static class ConstructorHandling // enum
    {
        static SIGNED_LONG_INTEGER Default;
        static SIGNED_LONG_INTEGER AllowNonPublicDefaultConstructor;
    };

    static class DateFormatHandling // enum
    {
        static SIGNED_LONG_INTEGER IsoDateFormat;
        static SIGNED_LONG_INTEGER MicrosoftDateFormat;
    };

    static class DateParseHandling // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER DateTime;
        static SIGNED_LONG_INTEGER DateTimeOffset;
    };

    static class DateTimeZoneHandling // enum
    {
        static SIGNED_LONG_INTEGER Local;
        static SIGNED_LONG_INTEGER Utc;
        static SIGNED_LONG_INTEGER Unspecified;
        static SIGNED_LONG_INTEGER RoundtripKind;
    };

     class DefaultJsonNameTable 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION Add ( STRING key );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

    static class DefaultValueHandling // enum
    {
        static SIGNED_LONG_INTEGER Include;
        static SIGNED_LONG_INTEGER Ignore;
        static SIGNED_LONG_INTEGER Populate;
        static SIGNED_LONG_INTEGER IgnoreAndPopulate;
    };

    static class FloatFormatHandling // enum
    {
        static SIGNED_LONG_INTEGER String;
        static SIGNED_LONG_INTEGER Symbol;
        static SIGNED_LONG_INTEGER DefaultValue;
    };

    static class FloatParseHandling // enum
    {
        static SIGNED_LONG_INTEGER Double;
        static SIGNED_LONG_INTEGER Decimal;
    };

    static class Formatting // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER Indented;
    };

     class JsonArrayAttribute 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Id[];
        STRING Title[];
        STRING Description[];
        ReferenceLoopHandling ItemReferenceLoopHandling;
        TypeNameHandling ItemTypeNameHandling;
    };

     class JsonConstructorAttribute 
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

     class JsonContainerAttribute 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Id[];
        STRING Title[];
        STRING Description[];
        ReferenceLoopHandling ItemReferenceLoopHandling;
        TypeNameHandling ItemTypeNameHandling;
    };

    static class JsonConvert 
    {
        // class delegates

        // class events

        // class functions
        static STRING_FUNCTION ToString ( SIGNED_LONG_INTEGER value );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING True[];
        static STRING False[];
        static STRING Null[];
        static STRING Undefined[];
        static STRING PositiveInfinity[];
        static STRING NegativeInfinity[];
        static STRING NaN[];

        // class properties
    };

     class JsonConverter 
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

     class JsonConverterCollection 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION set_Item ( SIGNED_LONG_INTEGER index , JsonConverter value );
        FUNCTION Add ( JsonConverter item );
        FUNCTION Clear ();
        FUNCTION CopyTo ( JsonConverter array[] , SIGNED_LONG_INTEGER index );
        SIGNED_LONG_INTEGER_FUNCTION IndexOf ( JsonConverter item );
        FUNCTION Insert ( SIGNED_LONG_INTEGER index , JsonConverter item );
        FUNCTION RemoveAt ( SIGNED_LONG_INTEGER index );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER Count;
    };

     class JsonDictionaryAttribute 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Id[];
        STRING Title[];
        STRING Description[];
        ReferenceLoopHandling ItemReferenceLoopHandling;
        TypeNameHandling ItemTypeNameHandling;
    };

     class JsonException 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Message[];
        STRING StackTrace[];
        STRING HelpLink[];
        STRING Source[];
        SIGNED_LONG_INTEGER HResult;
    };

     class JsonExtensionDataAttribute 
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

     class JsonIgnoreAttribute 
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

     class JsonNameTable 
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

     class JsonObjectAttribute 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        MemberSerialization MemberSerialization;
        MissingMemberHandling MissingMemberHandling;
        NullValueHandling ItemNullValueHandling;
        Required ItemRequired;
        STRING Id[];
        STRING Title[];
        STRING Description[];
        ReferenceLoopHandling ItemReferenceLoopHandling;
        TypeNameHandling ItemTypeNameHandling;
    };

     class JsonPropertyAttribute 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        NullValueHandling NullValueHandling;
        DefaultValueHandling DefaultValueHandling;
        ReferenceLoopHandling ReferenceLoopHandling;
        ObjectCreationHandling ObjectCreationHandling;
        TypeNameHandling TypeNameHandling;
        SIGNED_LONG_INTEGER Order;
        Required Required;
        STRING PropertyName[];
        ReferenceLoopHandling ItemReferenceLoopHandling;
        TypeNameHandling ItemTypeNameHandling;
    };

     class JsonReader 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ReadAsString ();
        FUNCTION Skip ();
        FUNCTION Close ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        DateTimeZoneHandling DateTimeZoneHandling;
        DateParseHandling DateParseHandling;
        FloatParseHandling FloatParseHandling;
        STRING DateFormatString[];
        JsonToken TokenType;
        SIGNED_LONG_INTEGER Depth;
        STRING Path[];
    };

     class JsonReaderException 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER LineNumber;
        SIGNED_LONG_INTEGER LinePosition;
        STRING Path[];
        STRING Message[];
        STRING StackTrace[];
        STRING HelpLink[];
        STRING Source[];
        SIGNED_LONG_INTEGER HResult;
    };

     class JsonRequiredAttribute 
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

     class JsonSerializationException 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER LineNumber;
        SIGNED_LONG_INTEGER LinePosition;
        STRING Path[];
        STRING Message[];
        STRING StackTrace[];
        STRING HelpLink[];
        STRING Source[];
        SIGNED_LONG_INTEGER HResult;
    };

     class JsonSerializer 
    {
        // class delegates

        // class events
        EventHandler Error ( JsonSerializer sender, ErrorEventArgs e );

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        TypeNameHandling TypeNameHandling;
        TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling;
        PreserveReferencesHandling PreserveReferencesHandling;
        ReferenceLoopHandling ReferenceLoopHandling;
        MissingMemberHandling MissingMemberHandling;
        NullValueHandling NullValueHandling;
        DefaultValueHandling DefaultValueHandling;
        ObjectCreationHandling ObjectCreationHandling;
        ConstructorHandling ConstructorHandling;
        MetadataPropertyHandling MetadataPropertyHandling;
        JsonConverterCollection Converters;
        Formatting Formatting;
        DateFormatHandling DateFormatHandling;
        DateTimeZoneHandling DateTimeZoneHandling;
        DateParseHandling DateParseHandling;
        FloatParseHandling FloatParseHandling;
        FloatFormatHandling FloatFormatHandling;
        StringEscapeHandling StringEscapeHandling;
        STRING DateFormatString[];
    };

     class JsonSerializerSettings 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        ReferenceLoopHandling ReferenceLoopHandling;
        MissingMemberHandling MissingMemberHandling;
        ObjectCreationHandling ObjectCreationHandling;
        NullValueHandling NullValueHandling;
        DefaultValueHandling DefaultValueHandling;
        PreserveReferencesHandling PreserveReferencesHandling;
        TypeNameHandling TypeNameHandling;
        MetadataPropertyHandling MetadataPropertyHandling;
        TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling;
        ConstructorHandling ConstructorHandling;
        STRING DateFormatString[];
        Formatting Formatting;
        DateFormatHandling DateFormatHandling;
        DateTimeZoneHandling DateTimeZoneHandling;
        DateParseHandling DateParseHandling;
        FloatFormatHandling FloatFormatHandling;
        FloatParseHandling FloatParseHandling;
        StringEscapeHandling StringEscapeHandling;
    };

    static class JsonToken // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER StartObject;
        static SIGNED_LONG_INTEGER StartArray;
        static SIGNED_LONG_INTEGER StartConstructor;
        static SIGNED_LONG_INTEGER PropertyName;
        static SIGNED_LONG_INTEGER Comment;
        static SIGNED_LONG_INTEGER Raw;
        static SIGNED_LONG_INTEGER Integer;
        static SIGNED_LONG_INTEGER Float;
        static SIGNED_LONG_INTEGER String;
        static SIGNED_LONG_INTEGER Boolean;
        static SIGNED_LONG_INTEGER Null;
        static SIGNED_LONG_INTEGER Undefined;
        static SIGNED_LONG_INTEGER EndObject;
        static SIGNED_LONG_INTEGER EndArray;
        static SIGNED_LONG_INTEGER EndConstructor;
        static SIGNED_LONG_INTEGER Date;
        static SIGNED_LONG_INTEGER Bytes;
    };

     class JsonWriter 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Flush ();
        FUNCTION Close ();
        FUNCTION WriteStartObject ();
        FUNCTION WriteEndObject ();
        FUNCTION WriteStartArray ();
        FUNCTION WriteEndArray ();
        FUNCTION WriteStartConstructor ( STRING name );
        FUNCTION WriteEndConstructor ();
        FUNCTION WritePropertyName ( STRING name );
        FUNCTION WriteEnd ();
        FUNCTION WriteToken ( JsonReader reader );
        FUNCTION WriteNull ();
        FUNCTION WriteUndefined ();
        FUNCTION WriteRaw ( STRING json );
        FUNCTION WriteRawValue ( STRING json );
        FUNCTION WriteValue ( STRING value );
        FUNCTION WriteComment ( STRING text );
        FUNCTION WriteWhitespace ( STRING ws );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        WriteState WriteState;
        STRING Path[];
        Formatting Formatting;
        DateFormatHandling DateFormatHandling;
        DateTimeZoneHandling DateTimeZoneHandling;
        StringEscapeHandling StringEscapeHandling;
        FloatFormatHandling FloatFormatHandling;
        STRING DateFormatString[];
    };

     class JsonWriterException 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Path[];
        STRING Message[];
        STRING StackTrace[];
        STRING HelpLink[];
        STRING Source[];
        SIGNED_LONG_INTEGER HResult;
    };

    static class MemberSerialization // enum
    {
        static SIGNED_LONG_INTEGER OptOut;
        static SIGNED_LONG_INTEGER OptIn;
        static SIGNED_LONG_INTEGER Fields;
    };

    static class MetadataPropertyHandling // enum
    {
        static SIGNED_LONG_INTEGER Default;
        static SIGNED_LONG_INTEGER ReadAhead;
        static SIGNED_LONG_INTEGER Ignore;
    };

    static class MissingMemberHandling // enum
    {
        static SIGNED_LONG_INTEGER Ignore;
        static SIGNED_LONG_INTEGER Error;
    };

    static class NullValueHandling // enum
    {
        static SIGNED_LONG_INTEGER Include;
        static SIGNED_LONG_INTEGER Ignore;
    };

    static class ObjectCreationHandling // enum
    {
        static SIGNED_LONG_INTEGER Auto;
        static SIGNED_LONG_INTEGER Reuse;
        static SIGNED_LONG_INTEGER Replace;
    };

    static class PreserveReferencesHandling // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER Objects;
        static SIGNED_LONG_INTEGER Arrays;
        static SIGNED_LONG_INTEGER All;
    };

    static class ReferenceLoopHandling // enum
    {
        static SIGNED_LONG_INTEGER Error;
        static SIGNED_LONG_INTEGER Ignore;
        static SIGNED_LONG_INTEGER Serialize;
    };

    static class Required // enum
    {
        static SIGNED_LONG_INTEGER Default;
        static SIGNED_LONG_INTEGER AllowNull;
        static SIGNED_LONG_INTEGER Always;
        static SIGNED_LONG_INTEGER DisallowNull;
    };

    static class StringEscapeHandling // enum
    {
        static SIGNED_LONG_INTEGER Default;
        static SIGNED_LONG_INTEGER EscapeNonAscii;
        static SIGNED_LONG_INTEGER EscapeHtml;
    };

    static class TypeNameAssemblyFormatHandling // enum
    {
        static SIGNED_LONG_INTEGER Simple;
        static SIGNED_LONG_INTEGER Full;
    };

    static class TypeNameHandling // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER Objects;
        static SIGNED_LONG_INTEGER Arrays;
        static SIGNED_LONG_INTEGER All;
        static SIGNED_LONG_INTEGER Auto;
    };

    static class WriteState // enum
    {
        static SIGNED_LONG_INTEGER Error;
        static SIGNED_LONG_INTEGER Closed;
        static SIGNED_LONG_INTEGER Object;
        static SIGNED_LONG_INTEGER Array;
        static SIGNED_LONG_INTEGER Constructor;
        static SIGNED_LONG_INTEGER Property;
        static SIGNED_LONG_INTEGER Start;
    };

namespace Newtonsoft.Json.Serialization;
        // class declarations
         class CamelCaseNamingStrategy;
         class CamelCasePropertyNamesContractResolver;
         class DefaultContractResolver;
         class DefaultNamingStrategy;
         class DefaultSerializationBinder;
         class DiagnosticsTraceWriter;
         class DynamicValueProvider;
         class ErrorContext;
         class ErrorEventArgs;
         class ExpressionValueProvider;
         class JsonArrayContract;
         class JsonContainerContract;
         class JsonContract;
         class JsonDictionaryContract;
         class JsonDynamicContract;
         class JsonISerializableContract;
         class JsonLinqContract;
         class JsonObjectContract;
         class JsonPrimitiveContract;
         class JsonProperty;
         class JsonPropertyCollection;
         class JsonStringContract;
         class KebabCaseNamingStrategy;
         class MemoryTraceWriter;
         class NamingStrategy;
         class OnErrorAttribute;
         class ReflectionAttributeProvider;
         class ReflectionValueProvider;
         class SnakeCaseNamingStrategy;
         class CreatorPropertyContext;
     class CamelCaseNamingStrategy 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetExtensionDataName ( STRING name );
        STRING_FUNCTION GetDictionaryKey ( STRING key );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class CamelCasePropertyNamesContractResolver 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetResolvedPropertyName ( STRING propertyName );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        NamingStrategy NamingStrategy;
    };

     class DefaultContractResolver 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetResolvedPropertyName ( STRING propertyName );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        NamingStrategy NamingStrategy;
    };

     class DefaultNamingStrategy 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetExtensionDataName ( STRING name );
        STRING_FUNCTION GetDictionaryKey ( STRING key );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class DefaultSerializationBinder 
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

     class DiagnosticsTraceWriter 
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

     class ErrorContext 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Path[];
    };

     class JsonContainerContract 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        JsonConverter ItemConverter;
        JsonConverter Converter;
        JsonConverter InternalConverter;
    };

     class JsonContract 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        JsonConverter Converter;
        JsonConverter InternalConverter;
    };

     class JsonProperty 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING PropertyName[];
        STRING UnderlyingName[];
        JsonConverter Converter;
        JsonConverter MemberConverter;
        Required Required;
        JsonConverter ItemConverter;
    };

     class KebabCaseNamingStrategy 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetExtensionDataName ( STRING name );
        STRING_FUNCTION GetDictionaryKey ( STRING key );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class MemoryTraceWriter 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class NamingStrategy 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetExtensionDataName ( STRING name );
        STRING_FUNCTION GetDictionaryKey ( STRING key );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class OnErrorAttribute 
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

     class SnakeCaseNamingStrategy 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetExtensionDataName ( STRING name );
        STRING_FUNCTION GetDictionaryKey ( STRING key );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

namespace Newtonsoft.Json.Schema;
        // class declarations
         class Extensions;
         class JsonSchema;
         class JsonSchemaException;
         class JsonSchemaGenerator;
         class JsonSchemaResolver;
         class JsonSchemaType;
         class UndefinedSchemaIdHandling;
         class ValidationEventArgs;
           class JToken;
    static class Extensions 
    {
        // class delegates

        // class events

        // class functions
        static FUNCTION Validate ( JToken source , JsonSchema schema );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class JsonSchema 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION WriteTo ( JsonWriter writer );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Id[];
        STRING Title[];
        STRING Description[];
        STRING Pattern[];
        JsonSchema AdditionalItems;
        JsonSchema AdditionalProperties;
        STRING Requires[];
        JToken Default;
        STRING Format[];
    };

     class JsonSchemaException 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER LineNumber;
        SIGNED_LONG_INTEGER LinePosition;
        STRING Path[];
        STRING Message[];
        STRING StackTrace[];
        STRING HelpLink[];
        STRING Source[];
        SIGNED_LONG_INTEGER HResult;
    };

     class JsonSchemaGenerator 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        UndefinedSchemaIdHandling UndefinedSchemaIdHandling;
    };

     class JsonSchemaResolver 
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

    static class JsonSchemaType // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER String;
        static SIGNED_LONG_INTEGER Float;
        static SIGNED_LONG_INTEGER Integer;
        static SIGNED_LONG_INTEGER Boolean;
        static SIGNED_LONG_INTEGER Object;
        static SIGNED_LONG_INTEGER Array;
        static SIGNED_LONG_INTEGER Null;
        static SIGNED_LONG_INTEGER Any;
    };

    static class UndefinedSchemaIdHandling // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER UseTypeName;
        static SIGNED_LONG_INTEGER UseAssemblyQualifiedName;
    };

     class ValidationEventArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        JsonSchemaException Exception;
        STRING Path[];
        STRING Message[];
    };

namespace Newtonsoft.Json.Linq;
        // class declarations
         class CommentHandling;
         class DuplicatePropertyNameHandling;
         class Extensions;
         class JArray;
         class JConstructor;
         class JContainer;
         class JObject;
         class JProperty;
         class JPropertyDescriptor;
         class JRaw;
         class JsonCloneSettings;
         class JsonLoadSettings;
         class JsonMergeSettings;
         class JsonSelectSettings;
         class JToken;
         class JTokenEqualityComparer;
         class JTokenReader;
         class JTokenType;
         class JTokenWriter;
         class JValue;
         class LineInfoHandling;
         class MergeArrayHandling;
         class MergeNullValueHandling;
    static class CommentHandling // enum
    {
        static SIGNED_LONG_INTEGER Ignore;
        static SIGNED_LONG_INTEGER Load;
    };

    static class DuplicatePropertyNameHandling // enum
    {
        static SIGNED_LONG_INTEGER Replace;
        static SIGNED_LONG_INTEGER Ignore;
        static SIGNED_LONG_INTEGER Error;
    };

    static class Extensions 
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

     class JArray 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION WriteTo ( JsonWriter writer , JsonConverter converters[] );
        FUNCTION set_Item ( SIGNED_LONG_INTEGER index , JToken value );
        SIGNED_LONG_INTEGER_FUNCTION IndexOf ( JToken item );
        FUNCTION Insert ( SIGNED_LONG_INTEGER index , JToken item );
        FUNCTION RemoveAt ( SIGNED_LONG_INTEGER index );
        FUNCTION Add ( JToken item );
        FUNCTION Clear ();
        FUNCTION CopyTo ( JToken array[] , SIGNED_LONG_INTEGER arrayIndex );
        FUNCTION RemoveAll ();
        FUNCTION RemoveAnnotations ();
        FUNCTION Remove ();
        FUNCTION Replace ( JToken value );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        JTokenType Type;
        JToken First;
        JToken Last;
        SIGNED_LONG_INTEGER Count;
        JTokenEqualityComparer EqualityComparer;
        JContainer Parent;
        JToken Root;
        JToken Next;
        JToken Previous;
        STRING Path[];
    };

     class JConstructor 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION WriteTo ( JsonWriter writer , JsonConverter converters[] );
        FUNCTION RemoveAll ();
        FUNCTION RemoveAnnotations ();
        FUNCTION Remove ();
        FUNCTION Replace ( JToken value );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Name[];
        JTokenType Type;
        JToken First;
        JToken Last;
        SIGNED_LONG_INTEGER Count;
        JTokenEqualityComparer EqualityComparer;
        JContainer Parent;
        JToken Root;
        JToken Next;
        JToken Previous;
        STRING Path[];
    };

     class JContainer 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION RemoveAll ();
        FUNCTION RemoveAnnotations ();
        FUNCTION Remove ();
        FUNCTION Replace ( JToken value );
        FUNCTION WriteTo ( JsonWriter writer , JsonConverter converters[] );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        JToken First;
        JToken Last;
        SIGNED_LONG_INTEGER Count;
        JTokenEqualityComparer EqualityComparer;
        JContainer Parent;
        JToken Root;
        JTokenType Type;
        JToken Next;
        JToken Previous;
        STRING Path[];
    };

     class JObject 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION set_Item ( STRING propertyName , JToken value );
        FUNCTION WriteTo ( JsonWriter writer , JsonConverter converters[] );
        FUNCTION Add ( STRING propertyName , JToken value );
        FUNCTION RemoveAll ();
        FUNCTION RemoveAnnotations ();
        FUNCTION Remove ();
        FUNCTION Replace ( JToken value );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        JTokenType Type;
        JToken First;
        JToken Last;
        SIGNED_LONG_INTEGER Count;
        JTokenEqualityComparer EqualityComparer;
        JContainer Parent;
        JToken Root;
        JToken Next;
        JToken Previous;
        STRING Path[];
    };

     class JsonCloneSettings 
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

     class JsonLoadSettings 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        CommentHandling CommentHandling;
        LineInfoHandling LineInfoHandling;
        DuplicatePropertyNameHandling DuplicatePropertyNameHandling;
    };

     class JsonMergeSettings 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        MergeArrayHandling MergeArrayHandling;
        MergeNullValueHandling MergeNullValueHandling;
    };

     class JsonSelectSettings 
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

     class JToken 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION RemoveAnnotations ();
        FUNCTION Remove ();
        FUNCTION Replace ( JToken value );
        FUNCTION WriteTo ( JsonWriter writer , JsonConverter converters[] );
        STRING_FUNCTION ToString ();
        static SIGNED_LONG_INTEGER_FUNCTION op_Explicit ( JToken value );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        JTokenEqualityComparer EqualityComparer;
        JContainer Parent;
        JToken Root;
        JTokenType Type;
        JToken Next;
        JToken Previous;
        STRING Path[];
        JToken First;
        JToken Last;
    };

     class JTokenEqualityComparer 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ( JToken obj );
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

    static class JTokenType // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER Object;
        static SIGNED_LONG_INTEGER Array;
        static SIGNED_LONG_INTEGER Constructor;
        static SIGNED_LONG_INTEGER Property;
        static SIGNED_LONG_INTEGER Comment;
        static SIGNED_LONG_INTEGER Integer;
        static SIGNED_LONG_INTEGER Float;
        static SIGNED_LONG_INTEGER String;
        static SIGNED_LONG_INTEGER Boolean;
        static SIGNED_LONG_INTEGER Null;
        static SIGNED_LONG_INTEGER Undefined;
        static SIGNED_LONG_INTEGER Date;
        static SIGNED_LONG_INTEGER Raw;
        static SIGNED_LONG_INTEGER Bytes;
        static SIGNED_LONG_INTEGER Guid;
        static SIGNED_LONG_INTEGER Uri;
        static SIGNED_LONG_INTEGER TimeSpan;
    };

     class JTokenWriter 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Flush ();
        FUNCTION Close ();
        FUNCTION WriteStartObject ();
        FUNCTION WriteStartArray ();
        FUNCTION WriteStartConstructor ( STRING name );
        FUNCTION WritePropertyName ( STRING name );
        FUNCTION WriteNull ();
        FUNCTION WriteUndefined ();
        FUNCTION WriteRaw ( STRING json );
        FUNCTION WriteComment ( STRING text );
        FUNCTION WriteValue ( STRING value );
        FUNCTION WriteEndObject ();
        FUNCTION WriteEndArray ();
        FUNCTION WriteEndConstructor ();
        FUNCTION WriteEnd ();
        FUNCTION WriteToken ( JsonReader reader );
        FUNCTION WriteRawValue ( STRING json );
        FUNCTION WriteWhitespace ( STRING ws );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        JToken CurrentToken;
        JToken Token;
        WriteState WriteState;
        STRING Path[];
        Formatting Formatting;
        DateFormatHandling DateFormatHandling;
        DateTimeZoneHandling DateTimeZoneHandling;
        StringEscapeHandling StringEscapeHandling;
        FloatFormatHandling FloatFormatHandling;
        STRING DateFormatString[];
    };

    static class LineInfoHandling // enum
    {
        static SIGNED_LONG_INTEGER Ignore;
        static SIGNED_LONG_INTEGER Load;
    };

    static class MergeArrayHandling // enum
    {
        static SIGNED_LONG_INTEGER Concat;
        static SIGNED_LONG_INTEGER Union;
        static SIGNED_LONG_INTEGER Replace;
        static SIGNED_LONG_INTEGER Merge;
    };

    static class MergeNullValueHandling // enum
    {
        static SIGNED_LONG_INTEGER Ignore;
        static SIGNED_LONG_INTEGER Merge;
    };

namespace Newtonsoft.Json.Converters;
        // class declarations
         class BinaryConverter;
         class BsonObjectIdConverter;
         class DataSetConverter;
         class DataTableConverter;
         class DateTimeConverterBase;
         class DiscriminatedUnionConverter;
         class EntityKeyMemberConverter;
         class ExpandoObjectConverter;
         class IsoDateTimeConverter;
         class JavaScriptDateTimeConverter;
         class KeyValuePairConverter;
         class RegexConverter;
         class StringEnumConverter;
         class UnixDateTimeConverter;
         class VersionConverter;
         class XmlNodeConverter;
         class Union;
         class UnionCase;
     class BinaryConverter 
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

     class BsonObjectIdConverter 
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

     class DataSetConverter 
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

     class DataTableConverter 
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

     class DateTimeConverterBase 
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

     class DiscriminatedUnionConverter 
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

     class EntityKeyMemberConverter 
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

     class ExpandoObjectConverter 
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

     class IsoDateTimeConverter 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING DateTimeFormat[];
    };

     class JavaScriptDateTimeConverter 
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

     class KeyValuePairConverter 
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

     class RegexConverter 
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

     class StringEnumConverter 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        NamingStrategy NamingStrategy;
    };

     class UnixDateTimeConverter 
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

     class VersionConverter 
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

     class XmlNodeConverter 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING DeserializeRootElementName[];
    };

namespace Newtonsoft.Json.Bson;
        // class declarations
         class BsonObjectId;
         class BsonReader;
         class BsonWriter;

namespace Newtonsoft.Json.Utilities;
        // class declarations
         class BinderWrapper;
         class ImmutableCollectionTypeInfo;
    static class BinderWrapper 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        static STRING CSharpAssemblyName[];

        // class properties
    };


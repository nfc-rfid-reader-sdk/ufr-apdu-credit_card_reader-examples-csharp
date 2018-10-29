

using System;
using System.Runtime.InteropServices;
using emv_tag_index_t = System.Byte;
using emv_tag_t = System.Int32;





namespace uFrAdvance
{
    
    using DL_STATUS = System.UInt32;

    public enum UFR_STATUS
    {

        DL_OK,
        COMMUNICATION_ERROR = 0x01,
        CHKSUM_ERROR = 0x02,
        READING_ERROR = 0x03,
        WRITING_ERROR = 0x04,
        BUFFER_OVERFLOW = 0x05,
        MAX_ADDRESS_EXCEEDED = 0x06,
        MAX_KEY_INDEX_EXCEEDED = 0x07,
        NO_CARD = 0x08,
        COMMAND_NOT_SUPPORTED = 0x09,
        FORBIDEN_DIRECT_WRITE_IN_SECTOR_TRAILER = 0x0A,
        ADDRESSED_BLOCK_IS_NOT_SECTOR_TRAILER = 0x0B,
        WRONG_ADDRESS_MODE = 0x0C,
        WRONG_ACCESS_BITS_VALUES = 0x0D,
        AUTH_ERROR = 0x0E,
        PARAMETERS_ERROR = 0x0F,
        MAX_SIZE_EXCEEDED = 0x10,
        UNSUPPORTED_CARD_TYPE = 0x11,

        COMMUNICATION_BREAK = 0x50,
        NO_MEMORY_ERROR = 0x51,
        CAN_NOT_OPEN_READER = 0x52,
        READER_NOT_SUPPORTED = 0x53,
        READER_OPENING_ERROR = 0x54,
        READER_PORT_NOT_OPENED = 0x55,
        CANT_CLOSE_READER_PORT = 0x56,

        WRITE_VERIFICATION_ERROR = 0x70,
        BUFFER_SIZE_EXCEEDED = 0x71,
        VALUE_BLOCK_INVALID = 0x72,
        VALUE_BLOCK_ADDR_INVALID = 0x73,
        VALUE_BLOCK_MANIPULATION_ERROR = 0x74,
        WRONG_UI_MODE = 0x75,
        KEYS_LOCKED = 0x76,
        KEYS_UNLOCKED = 0x77,
        WRONG_PASSWORD = 0x78,
        CAN_NOT_LOCK_DEVICE = 0x79,
        CAN_NOT_UNLOCK_DEVICE = 0x7A,
        DEVICE_EEPROM_BUSY = 0x7B,
        RTC_SET_ERROR = 0x7C,
        ANTICOLLISION_DISABLED = 0x7D,
        NO_CARDS_ENUMERRATED = 0x7E,
        CARD_ALREADY_SELECTED = 0x7F,

        FT_STATUS_ERROR_1 = 0xA0,
        FT_STATUS_ERROR_2 = 0xA1,
        FT_STATUS_ERROR_3 = 0xA2,
        FT_STATUS_ERROR_4 = 0xA3,
        FT_STATUS_ERROR_5 = 0xA4,
        FT_STATUS_ERROR_6 = 0xA5,
        FT_STATUS_ERROR_7 = 0xA6,
        FT_STATUS_ERROR_8 = 0xA7,
        FT_STATUS_ERROR_9 = 0xA8,
        UFR_APDU_TRANSCEIVE_ERROR = 0xAE,
        UFR_APDU_JC_APP_NOT_SELECTED = 0x6000,
        UFR_APDU_JC_APP_BUFF_EMPTY,
        UFR_APDU_WRONG_SELECT_RESPONSE,
        UFR_APDU_WRONG_KEY_TYPE,
        UFR_APDU_WRONG_KEY_SIZE,
        UFR_APDU_WRONG_KEY_PARAMS,
        UFR_APDU_WRONG_ALGORITHM,
        UFR_APDU_PLAIN_TEXT_SIZE_EXCEEDED,
        UFR_APDU_UNSUPPORTED_KEY_SIZE,
        UFR_APDU_UNSUPPORTED_ALGORITHMS,
        UFR_APDU_RECORD_NOT_FOUND,
        UFR_APDU_SW_TAG = 0x0A0000

    }


    public enum EMV_STATUS
    {
        EMV_OK,
        SYS_ERR_OUT_OF_MEMORY,
        EMV_ERR_WRONG_INPUT_DATA,
        EMV_ERR_MAX_TAG_LEN_BYTES_EXCEEDED,
        EMV_ERR_TAG_NOT_FOUND,
        EMV_ERR_TAG_WRONG_SIZE,
        EMV_ERR_TAG_WRONG_TYPE,
        EMV_ERR_IN_CARD_READER,
        EMV_ERR_READING_RECORD,
        EMV_ERR_PDOL_IS_EMPTY,
        EMV_ERR_LIST_FORMAT_NOT_FOUND
    }

    public enum tag_type_t
    {
        STR = 10,
        LANGUAGE_CODE_PAIRS,
        BCD_4BY4,
        DEC_UINT8,
        DEC_UINT16,
        DEC_UINT32,
        ISO3166_COUNTRY,
        ISO4217_CURRENCY,
        DATE_YMD,
        BIN_OR_STR,
        BIN,
        //-------------------
        TL_LIST,
        NODE,
    }

    public unsafe class iso4217_currency_code_s
    {
        public ushort num_code;

        public string alpha_code;

        public string currency;

        public iso4217_currency_code_s(ushort num_code_init, string alpha_code_init, string currency_init)
        {
            num_code = num_code_init;

            alpha_code = alpha_code_init;

            currency = currency_init;
        }
    }


    public unsafe class emv_tags_s
    {
        public emv_tag_t tag;

        public string description;

        public tag_type_t tag_type;

        public byte tag_id_len;

        public emv_tags_s(emv_tag_t tag_init, string init_desc, tag_type_t init_tag_type, byte init_tag_id_len)
        {
            tag = tag_init;

            description = init_desc;

            tag_type = init_tag_type;

            tag_id_len = init_tag_id_len;
        }

    }

    public unsafe class emv_tree_node_t : emv_tree_node_s { };


    public unsafe class emv_tree_node_s
    {
        public emv_tag_t tag { get; set; }

        public byte tag_bytes { get; set; }

        public string description { get; set; }

        public tag_type_t tag_type { get; set; }

        public bool is_node_type { get; set; }

        public byte[] value;

        public int value_len { get; set; }

            public emv_tree_node_t tl_list_format;
            public emv_tree_node_t next;
            public emv_tree_node_t subnode;


    }

    public class afl_list_item_t : afl_list_item_s { };

    public class afl_list_item_s
    {
        public byte sfi;

        public byte record_first;

        public byte record_last;

        public byte record_num_offline_auth;

            public afl_list_item_t next;

    }

    public unsafe class uFCoder
    {

#if WIN64

        const string DLL_PATH = "..\\..\\ufr-lib\\windows\\x86_64\\";
        const string NAME_DLL = "uFCoder-x86_64.dll";

#else
        const string DLL_PATH = "..\\..\\ufr-lib\\windows\\x86\\";
      
        const string NAME_DLL = "uFCoder-x86.dll";
      

#endif
        const string DLL_NAME = DLL_PATH + NAME_DLL;
        

        public int MAX_TAG_LEN_BYTES = 3;
        

        public emv_tags_s[] emv_tags = {
        new emv_tags_s(0x9f01, "Acquirer Identifier", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f02, "Amount, Authorised (Numeric)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f03, "Amount, Other (Numeric)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f04, "Amount, Other (Binary)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f05, "Application Discretionary Data", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f06, "Application Identifier (AID) - terminal", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f07, "Application Usage Control", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f08, "Application Version Number", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f09, "Application Version Number", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f0b, "Cardholder Name Extended", tag_type_t.BIN, 2),
        new emv_tags_s(0xbf0c, "FCI Issuer Discretionary Data", tag_type_t.NODE, 2),
        new emv_tags_s(0x9f0d, "Issuer Action Code - Default", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f0e, "Issuer Action Code - Denial", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f0f, "Issuer Action Code - Online", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f10, "Issuer Application Data", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f11, "Issuer Code Table Index", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f12, "Application Preferred Name", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f13, "Last Online Application Transaction Counter (ATC) Register", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f14, "Lower Consecutive Offline Limit", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f15, "Merchant Category Code", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f16, "Merchant Identifier", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f17, "Personal Identification Number (PIN) Try Counter", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f18, "Issuer Script Identifier", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f1a, "Terminal Country Code", tag_type_t.ISO3166_COUNTRY, 2),
        new emv_tags_s(0x9f1b, "Terminal Floor Limit", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f1c, "Terminal Identification", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f1d, "Terminal Risk Management Data", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f1e, "Interface Device (IFD) Serial Number", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f1f, "Track 1 Discretionary Data", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f20, "Cardholder Name", tag_type_t.STR, 2),
        new emv_tags_s(0x9f21, "Transaction Time", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f22, "Certification Authority Public Key Index", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f23, "Upper Consecutive Offline Limit", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f24, "Application Expiration Date", tag_type_t.DATE_YMD, 2),
        new emv_tags_s(0x5f25, "Application Effective Date", tag_type_t.DATE_YMD, 2),
        new emv_tags_s(0x9f26, "Application Cryptogram", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f27, "Cryptogram Information Data", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f28, "Issuer Country Code", tag_type_t.ISO3166_COUNTRY, 2),
        new emv_tags_s(0x5f2a, "Transaction Currency Code", tag_type_t.ISO4217_CURRENCY, 2),
        new emv_tags_s(0x5f2d, "Language Preference", tag_type_t.LANGUAGE_CODE_PAIRS, 2),
        new emv_tags_s(0x9f2e, "Integrated Circuit Card (ICC) PIN Encipherment Public Key Exponent", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f2f, "Integrated Circuit Card (ICC) PIN Encipherment Public Key Remainder", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f30, "Service Code", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f32, "Issuer Public Key Exponent", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f33, "Terminal Capabilities", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f34, "Application PAN Sequence Number (PSN)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f35, "Terminal Type", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f36, "Transaction Currency Exponent", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f37, "Unpredictable Number", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f38, "Processing Options Data Object List (PDOL)", tag_type_t.TL_LIST, 2),
        new emv_tags_s(0x9f34, "Cardholder Verification Method (CVM) Results", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f3a, "Amount, Reference Currency", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f3b, "Application Reference Currency", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f3c, "Transaction Reference Currency Code", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f3d, "Transaction Reference Currency Exponent", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f40, "Additional Terminal Capabilities", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f41, "Transaction Sequence Counter", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f43, "Application Reference Currency Exponent", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f44, "Application Currency Exponent", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f2d, "Integrated Circuit Card (ICC) PIN Encipherment Public Key Certificate", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f46, "Integrated Circuit Card (ICC) Public Key Certificate", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f47, "Integrated Circuit Card (ICC) Public Key Exponent", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f48, "Integrated Circuit Card (ICC) Public Key Remainder", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f49, "Dynamic Data Authentication Data Object List (DDOL)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f4a, "Static Data Authentication Tag List", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f4b, "Signed Dynamic Application Data", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f4c, "ICC Dynamic Number", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f4d, "Log Entry", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f4e, "Merchant Name and Location", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f51, "Application Currency Code", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f52, "Card Verification Results (CVR)", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f53, "International Bank Account Number (IBAN)", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f54, "Bank Identifier Code (BIC)", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f55, "Issuer Country Code (alpha2 format)", tag_type_t.BIN, 2),
        new emv_tags_s(0x5f56, "Issuer Country Code (alpha3 format)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f58, "Lower Consecutive Offline Limit (Card Check)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f59, "Upper Consecutive Offline Limit (Card Check)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f5c, "Cumulative Total Transaction Amount Upper Limit", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f72, "Consecutive Transaction Limit (International - Country)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f7c, "Merchant Custom Data", tag_type_t.BIN, 2),
        new emv_tags_s(0x9F62, "PCVC3 (Track1)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9F63, "PUNATC (Track1)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9F64, "NATC (Track1)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f65, "Track 2 Bit Map for CVC3", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f66, "Terminal Transaction Qualifiers (TTQ)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f67, "NATC (Track2)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f68, "Mag Stripe CVM List", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f69, "Unpredictable Number Data Object List (UDOL)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f6b, "Track 2 Data", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f6c, "Mag Stripe Application Version Number (Card)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f6e, "Third Party Data", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f74, "VLP Issuer Authorization Code", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f75, "Cumulative Total Transaction Amount Limit - Dual Currency", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f76, "Secondary Application Currency Code", tag_type_t.ISO4217_CURRENCY, 2),
        new emv_tags_s(0x9f7d, "Unknown Tag", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f7f, "Card Production Life Cycle (CPLC) History File Identifiers", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f45, "Data Authentication Code", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f57, "Issuer Country Code", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f39, "Point-of-Service (POS) Entry Mode", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f73, "Currency Conversion Factor", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f42, "Application Currency Code", tag_type_t.ISO4217_CURRENCY, 2),
        new emv_tags_s(0x9f56, "Issuer Authentication Indicator", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f20, "Track 2 Discretionary Data", tag_type_t.BIN, 2),
        new emv_tags_s(0xdf01, "Reference PIN", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f36, "Application Transaction Counter (ATC)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f4f, "Log Format", tag_type_t.TL_LIST, 2),
        new emv_tags_s(0x5f50, "Issuer URL", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f5a, "Issuer URL2", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f53, "Consecutive Transaction Limit (International)", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f54, "Cumulative Total Transaction Amount Limit", tag_type_t.BIN, 2),
        new emv_tags_s(0x9f55, "Geographic Indicator", tag_type_t.BIN, 2),
        new emv_tags_s(0x42, "Issuer Identification Number (IIN)", tag_type_t.BIN, 1),
        new emv_tags_s(0x4f, "Application Identifier (AID)", tag_type_t.BIN, 1),
        new emv_tags_s(0x50, "Application Label", tag_type_t.STR, 1),
        new emv_tags_s(0x56, "Track 1 Equivalent Data", tag_type_t.BIN, 1),
        new emv_tags_s(0x57, "Track 2 Equivalent Data", tag_type_t.BIN, 1),
        new emv_tags_s(0x5a, "Application Primary Account Number (PAN)", tag_type_t.BCD_4BY4, 1),
        new emv_tags_s(0x61, "Application Template", tag_type_t.NODE, 1),
        new emv_tags_s(0x6f, "File Control Information (FCI) Template", tag_type_t.NODE, 1),
        new emv_tags_s(0x70, "Response Message Template / AEF Data Template", tag_type_t.NODE, 1),
        new emv_tags_s(0x71, "Issuer Script Template 1", tag_type_t.BIN, 1),
        new emv_tags_s(0x72, "Issuer Script Template 2", tag_type_t.BIN, 1),
        new emv_tags_s(0x73, "Directory Discretionary Template", tag_type_t.BIN, 1),
        new emv_tags_s(0x77, "Response Message Template Format 2", tag_type_t.NODE, 1),
        new emv_tags_s(0x80, "Response Message Template Format 1", tag_type_t.BIN, 1),
        new emv_tags_s(0x81, "Amount, Authorised (Binary)", tag_type_t.BIN, 1),
        new emv_tags_s(0x82, "Application Interchange Profile (AIP)", tag_type_t.BIN, 1),
        new emv_tags_s(0x83, "Command Template", tag_type_t.BIN, 1),
        new emv_tags_s(0x84, "Dedicated File (DF) Name", tag_type_t.BIN_OR_STR, 1),
        new emv_tags_s(0x86, "Issuer Script Command", tag_type_t.BIN, 1),
        new emv_tags_s(0x87, "Application Priority Indicator", tag_type_t.BIN, 1),
        new emv_tags_s(0x88, "Short File Identifier (SFI)", tag_type_t.BIN, 1),
        new emv_tags_s(0x89, "Authorisation Code", tag_type_t.BIN, 1),
        new emv_tags_s(0x8a, "Authorisation Response Code", tag_type_t.BIN, 1),
        new emv_tags_s(0x8c, "Card Risk Management Data Object List 1 (CDOL1)", tag_type_t.BIN, 1),
        new emv_tags_s(0x8d, "Card Risk Management Data Object List 2 (CDOL2)", tag_type_t.BIN, 1),
        new emv_tags_s(0x8e, "Cardholder Verification Method (CVM) List", tag_type_t.BIN, 1),
        new emv_tags_s(0x8f, "Certification Authority Public Key Index", tag_type_t.BIN, 1),
        new emv_tags_s(0x90, "Issuer Public Key Certificate", tag_type_t.BIN, 1),
        new emv_tags_s(0x91, "Issuer Authentication Data", tag_type_t.BIN, 1),
        new emv_tags_s(0x92, "Issuer Public Key Remainder", tag_type_t.BIN, 1),
        new emv_tags_s(0x93, "Signed Static Application Data", tag_type_t.BIN, 1),
        new emv_tags_s(0x94, "Application File Locator (AFL)", tag_type_t.BIN, 1),
        new emv_tags_s(0x95, "Terminal Verification Results", tag_type_t.BIN, 1),
        new emv_tags_s(0x97, "Transaction Certificate Data Object List (TDOL)", tag_type_t.BIN, 1),
        new emv_tags_s(0x98, "Transaction Certificate (TC) Hash Value", tag_type_t.BIN, 1),
        new emv_tags_s(0x99, "Transaction Personal Identification Number (PIN) Data", tag_type_t.BIN, 1),
        new emv_tags_s(0x9a, "Transaction Date", tag_type_t.DATE_YMD, 1),
        new emv_tags_s(0x9b, "Transaction Status Information", tag_type_t.BIN, 1),
        new emv_tags_s(0x9c, "Transaction Type", tag_type_t.BIN, 1),
        new emv_tags_s(0x9d, "Directory Definition File (DDF) Name", tag_type_t.BIN, 1),
        new emv_tags_s(0xa5, "File Control Information (FCI) Proprietary Template", tag_type_t.NODE, 1),
        new emv_tags_s(0, "UNKNOWN TAG", tag_type_t.BIN, 0)
        };


        public iso4217_currency_code_s[] iso4217_currency_codes =
        {
            new iso4217_currency_code_s( 8,   "ALL", "Albania Lek"),
            new iso4217_currency_code_s( 12,  "DZD", "Algeria Dinar" ),
            new iso4217_currency_code_s( 32,  "ARS", "Argentina Peso" ),
            new iso4217_currency_code_s( 36,  "AUD", "Australia Dollar" ),
            new iso4217_currency_code_s( 44,  "BSD", "Bahamas Dollar" ),
            new iso4217_currency_code_s( 48,  "BHD", "Bahrain Dinar" ),
            new iso4217_currency_code_s( 50,  "BDT", "Bangladesh Taka" ),
            new iso4217_currency_code_s( 51,  "AMD", "Armenia Dram" ),
            new iso4217_currency_code_s( 52,  "BBD", "Barbados Dollar" ),
            new iso4217_currency_code_s( 60,  "BMD", "Bermuda Dollar" ),
            new iso4217_currency_code_s( 64,  "BTN", "Bhutan Ngultrum" ),
            new iso4217_currency_code_s( 68,  "BOB", "Bolivia Bolíviano" ),
            new iso4217_currency_code_s( 72,  "BWP", "Botswana Pula" ),
            new iso4217_currency_code_s( 84,  "BZD", "Belize Dollar" ),
            new iso4217_currency_code_s( 90,  "SBD", "Solomon Islands Dollar" ),
            new iso4217_currency_code_s( 96,  "BND", "Brunei Darussalam Dollar" ),
            new iso4217_currency_code_s( 104, "MMK", "Myanmar (Burma) Kyat" ),
            new iso4217_currency_code_s( 108, "BIF", "Burundi Franc" ),
            new iso4217_currency_code_s( 116, "KHR", "Cambodia Riel" ),
            new iso4217_currency_code_s( 124, "CAD", "Canada Dollar" ),
            new iso4217_currency_code_s( 132, "CVE", "Cape Verde Escudo" ),
            new iso4217_currency_code_s( 136, "KYD", "Cayman Islands Dollar" ),
            new iso4217_currency_code_s( 144, "LKR", "Sri Lanka Rupee" ),
            new iso4217_currency_code_s( 152, "CLP", "Chile Peso" ),
            new iso4217_currency_code_s( 156, "CNY", "China Yuan Renminbi" ),
            new iso4217_currency_code_s( 170, "COP", "Colombia Peso" ),
            new iso4217_currency_code_s( 174, "KMF", "Comorian Franc" ),
            new iso4217_currency_code_s( 188, "CRC", "Costa Rica Colon" ),
            new iso4217_currency_code_s( 191, "HRK", "Croatia Kuna" ),
            new iso4217_currency_code_s( 192, "CUP", "Cuba Peso" ),
            new iso4217_currency_code_s( 203, "CZK", "Czech Republic Koruna" ),
            new iso4217_currency_code_s( 208, "DKK", "Denmark Krone" ),
            new iso4217_currency_code_s( 214, "DOP", "Dominican Republic Peso" ),
            new iso4217_currency_code_s( 222, "SVC", "El Salvador Colon" ),
            new iso4217_currency_code_s( 230, "ETB", "Ethiopia Birr" ),
            new iso4217_currency_code_s( 232, "ERN", "Eritrea Nakfa" ),
            new iso4217_currency_code_s( 238, "FKP", "Falkland Islands (Malvinas) Pound" ),
            new iso4217_currency_code_s( 242, "FJD", "Fiji Dollar" ),
            new iso4217_currency_code_s( 262, "DJF", "Djibouti Franc" ),
            new iso4217_currency_code_s( 270, "GMD", "Gambia Dalasi" ),
            new iso4217_currency_code_s( 292, "GIP", "Gibraltar Pound" ),
            new iso4217_currency_code_s( 320, "GTQ", "Guatemala Quetzal" ),
            new iso4217_currency_code_s( 324, "GNF", "Guinea Franc" ),
            new iso4217_currency_code_s( 328, "GYD", "Guyana Dollar" ),
            new iso4217_currency_code_s( 332, "HTG", "Haiti Gourde" ),
            new iso4217_currency_code_s( 340, "HNL", "Honduras Lempira" ),
            new iso4217_currency_code_s( 344, "HKD", "Hong Kong Dollar" ),
            new iso4217_currency_code_s( 348, "HUF", "Hungary Forint" ),
            new iso4217_currency_code_s( 352, "ISK", "Iceland Krona" ),
            new iso4217_currency_code_s( 356, "INR", "India Rupee" ),
            new iso4217_currency_code_s( 360, "IDR", "Indonesia Rupiah" ),
            new iso4217_currency_code_s( 364, "IRR", "Iran Rial" ),
            new iso4217_currency_code_s( 368, "IQD", "Iraq Dinar" ),
            new iso4217_currency_code_s( 376, "ILS", "Israel Shekel" ),
            new iso4217_currency_code_s( 388, "JMD", "Jamaica Dollar" ),
            new iso4217_currency_code_s( 392, "JPY", "Japan Yen" ),
            new iso4217_currency_code_s( 398, "KZT", "Kazakhstan Tenge" ),
            new iso4217_currency_code_s( 400, "JOD", "Jordan Dinar" ),
            new iso4217_currency_code_s( 404, "KES", "Kenya Shilling" ),
            new iso4217_currency_code_s( 408, "KPW", "Korea (North) Won" ),
            new iso4217_currency_code_s( 410, "KRW", "Korea (South) Won" ),
            new iso4217_currency_code_s( 414, "KWD", "Kuwait Dinar" ),
            new iso4217_currency_code_s( 417, "KGS", "Kyrgyzstan Som" ),
            new iso4217_currency_code_s( 418, "LAK", "Laos Kip" ),
            new iso4217_currency_code_s( 422, "LBP", "Lebanon Pound" ),
            new iso4217_currency_code_s( 426, "LSL", "Lesotho Loti" ),
            new iso4217_currency_code_s( 430, "LRD", "Liberia Dollar" ),
            new iso4217_currency_code_s( 434, "LYD", "Libya Dinar" ),
            new iso4217_currency_code_s( 446, "MOP", "Macau Pataca" ),
            new iso4217_currency_code_s( 454, "MWK", "Malawi Kwacha" ),
            new iso4217_currency_code_s( 458, "MYR", "Malaysia Ringgit" ),
            new iso4217_currency_code_s( 462, "MVR", "Maldives (Maldive Islands) Rufiyaa" ),
            new iso4217_currency_code_s( 478, "MRO", "Mauritania Ouguiya" ),
            new iso4217_currency_code_s( 480, "MUR", "Mauritius Rupee" ),
            new iso4217_currency_code_s( 484, "MXN", "Mexico Peso" ),
            new iso4217_currency_code_s( 496, "MNT", "Mongolia Tughrik" ),
            new iso4217_currency_code_s( 498, "MDL", "Moldova Leu" ),
            new iso4217_currency_code_s( 504, "MAD", "Morocco Dirham" ),
            new iso4217_currency_code_s( 512, "OMR", "Oman Rial" ),
            new iso4217_currency_code_s( 516, "NAD", "Namibia Dollar" ),
            new iso4217_currency_code_s( 524, "NPR", "Nepal Rupee" ),
            new iso4217_currency_code_s( 532, "ANG", "Netherlands Antilles Guilder" ),
            new iso4217_currency_code_s( 533, "AWG", "Aruba Guilder" ),
            new iso4217_currency_code_s( 548, "VUV", "Vanuatu Vatu" ),
            new iso4217_currency_code_s( 554, "NZD", "New Zealand Dollar" ),
            new iso4217_currency_code_s( 558, "NIO", "Nicaragua Cordoba" ),
            new iso4217_currency_code_s( 566, "NGN", "Nigeria Naira" ),
            new iso4217_currency_code_s( 578, "NOK", "Norway Krone" ),
            new iso4217_currency_code_s( 586, "PKR", "Pakistan Rupee" ),
            new iso4217_currency_code_s( 590, "PAB", "Panama Balboa" ),
            new iso4217_currency_code_s( 598, "PGK", "Papua New Guinea Kina" ),
            new iso4217_currency_code_s( 600, "PYG", "Paraguay Guarani" ),
            new iso4217_currency_code_s( 604, "PEN", "Peru Sol" ),
            new iso4217_currency_code_s( 608, "PHP", "Philippines Peso" ),
            new iso4217_currency_code_s( 634, "QAR", "Qatar Riyal" ),
            new iso4217_currency_code_s( 643, "RUB", "Russia Ruble" ),
            new iso4217_currency_code_s( 646, "RWF", "Rwanda Franc" ),
            new iso4217_currency_code_s( 654, "SHP", "Saint Helena Pound" ),
            new iso4217_currency_code_s( 678, "STD", "Sao Tome and Principe dobra" ),
            new iso4217_currency_code_s( 682, "SAR", "Saudi Arabia Riyal" ),
            new iso4217_currency_code_s( 690, "SCR", "Seychelles Rupee" ),
            new iso4217_currency_code_s( 694, "SLL", "Sierra Leone Leone" ),
            new iso4217_currency_code_s( 702, "SGD", "Singapore Dollar" ),
            new iso4217_currency_code_s( 704, "VND", "Viet Nam Dong" ),
            new iso4217_currency_code_s( 706, "SOS", "Somalia Shilling" ),
            new iso4217_currency_code_s( 710, "ZAR", "South Africa Rand" ),
            new iso4217_currency_code_s( 728, "SSP", "South Sudanese pound" ),
            new iso4217_currency_code_s( 748, "SZL", "Swaziland Lilangeni" ),
            new iso4217_currency_code_s( 752, "SEK", "Sweden Krona" ),
            new iso4217_currency_code_s( 756, "CHF", "Switzerland Franc" ),
            new iso4217_currency_code_s( 760, "SYP", "Syria Pound" ),
            new iso4217_currency_code_s( 764, "THB", "Thailand Baht" ),
            new iso4217_currency_code_s( 776, "TOP", "Tonga Pa‘anga" ),
            new iso4217_currency_code_s( 780, "TTD", "Trinidad and Tobago Dollar" ),
            new iso4217_currency_code_s( 784, "AED", "United Arab Emirates Dirham" ),
            new iso4217_currency_code_s( 788, "TND", "Tunisia Dinar" ),
            new iso4217_currency_code_s( 800, "UGX", "Uganda Shilling" ),
            new iso4217_currency_code_s( 807, "MKD", "Macedonia Denar" ),
            new iso4217_currency_code_s( 818, "EGP", "Egypt Pound" ),
            new iso4217_currency_code_s( 826, "GBP", "United Kingdom Pound" ),
            new iso4217_currency_code_s( 834, "TZS", "Tanzania Shilling" ),
            new iso4217_currency_code_s( 840, "USD", "United States Dollar" ),
            new iso4217_currency_code_s( 858, "UYU", "Uruguay Peso" ),
            new iso4217_currency_code_s( 860, "UZS", "Uzbekistan Som" ),
            new iso4217_currency_code_s( 882, "WST", "Samoa Tala" ),
            new iso4217_currency_code_s( 886, "YER", "Yemen Rial" ),
            new iso4217_currency_code_s( 901, "TWD", "Taiwan New Dollar" ),
            new iso4217_currency_code_s( 931, "CUC", "Cuba Convertible Peso" ),
            new iso4217_currency_code_s( 932, "ZWL", "Zimbabwe Dollar" ),
            new iso4217_currency_code_s( 933, "BYN", "Belarus Ruble" ),
            new iso4217_currency_code_s( 934, "TMT", "Turkmenistan Manat" ),
            new iso4217_currency_code_s( 936, "GHS", "Ghana Cedi" ),
            new iso4217_currency_code_s( 937, "VEF", "Venezuela Bolívar" ),
            new iso4217_currency_code_s( 938, "SDG", "Sudan Pound" ),
            new iso4217_currency_code_s( 941, "RSD", "Serbia Dinar" ),
            new iso4217_currency_code_s( 943, "MZN", "Mozambique Metical" ),
            new iso4217_currency_code_s( 944, "AZN", "Azerbaijan Manat" ),
            new iso4217_currency_code_s( 946, "RON", "Romania Leu" ),
            new iso4217_currency_code_s( 949, "TRY", "Turkey Lira" ),
            new iso4217_currency_code_s( 950, "XAF", "CFA franc BEAC" ),
            new iso4217_currency_code_s( 951, "XCD", "East Caribbean Dollar" ),
            new iso4217_currency_code_s( 952, "XOF", "CFA franc BCEAO" ),
            new iso4217_currency_code_s( 953, "XPF", "CFP franc (franc Pacifique)" ),
            new iso4217_currency_code_s( 960, "XDR", "IMF Special Drawing Rights" ),
            new iso4217_currency_code_s( 967, "ZMW", "Zambia Kwacha" ),
            new iso4217_currency_code_s( 968, "SRD", "Suriname Dollar" ),
            new iso4217_currency_code_s( 969, "MGA", "Madagascar Ariary" ),
            new iso4217_currency_code_s( 971, "AFN", "Afghanistan Afghani" ),
            new iso4217_currency_code_s( 972, "TJS", "Tajikistan Somoni" ),
            new iso4217_currency_code_s( 973, "AOA", "Angola Kwanza" ),
            new iso4217_currency_code_s( 975, "BGN", "Bulgaria Lev" ),
            new iso4217_currency_code_s( 976, "CDF", "Congo/Kinshasa Franc" ),
            new iso4217_currency_code_s( 977, "BAM", "Bosnia and Herzegovina Convertible Marka" ),
            new iso4217_currency_code_s( 978, "EUR", "Euro" ),
            new iso4217_currency_code_s( 980, "UAH", "Ukraine Hryvnia" ),
            new iso4217_currency_code_s( 981, "GEL", "Georgia Lari" ),
            new iso4217_currency_code_s( 985, "PLN", "Poland Zloty" ),
            new iso4217_currency_code_s( 986, "BRL", "Brazil Real" ),
            new iso4217_currency_code_s( 0, "---", "Unknown currency")
    };

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderOpen")]
        public static extern DL_STATUS ReaderOpen();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderClose")]
        public static extern DL_STATUS ReaderClose();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderReset")]
        public static extern DL_STATUS ReaderReset();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderSoftRestart")]
        public static extern DL_STATUS ReaderSoftRestart();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderType")]
        public static extern DL_STATUS GetReaderType(ulong* get_reader_type);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderKeyWrite")]
        public static extern DL_STATUS ReaderKeyWrite(byte* aucKey, byte ucKeyIndex);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderSerialNumber")]
        public static extern DL_STATUS GetReaderSerialNumber(ulong* serial_number);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetCardId")]
        public static extern DL_STATUS GetCardId(byte* card_type, ulong* card_serial);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetCardIdEx")]
        public static extern DL_STATUS GetCardIdEx(byte* bCardType,
                                                   byte* bCardUid,
                                                   byte* bUidSize);
        
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetDlogicCardType")]
        public static extern DL_STATUS GetDlogicCardType(byte* bCardType);
        
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderUISignal")]
        public static extern DL_STATUS ReaderUISignal(int light_mode, int sound_mode);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReadUserData")]
        public static extern DL_STATUS ReadUserData(byte* aucData);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "WriteUserData")]
        public static extern DL_STATUS WriteUserData(byte* aucData);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderHardwareVersion")]
        public static extern DL_STATUS GetReaderHardwareVersion(byte* bVerMajor,
                                                                byte* bVerMinor);
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderFirmwareVersion")]
        public static extern DL_STATUS GetReaderFirmwareVersion(byte* bVerMajor,
                                                               byte* bVerMinor);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderSerialDescription")]
        public static extern DL_STATUS GetReaderSerialDescription(byte[] SerialDescription);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetBuildNumber")]
        public static extern DL_STATUS GetBuildNumber(byte* build);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "UFR_Status2String")]
        private static extern IntPtr UFR_Status2String(DL_STATUS status);
        public static string status2str(DL_STATUS status)
        {
            IntPtr str_ret = UFR_Status2String(status);
            return Marshal.PtrToStringAnsi(str_ret);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetDllVersionStr")]
        private static extern IntPtr GetDllVersionStr();
        public static string GetLibraryVersion()
        {
            IntPtr str_ret = GetDllVersionStr();
            return Marshal.PtrToStringAnsi(str_ret);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "SetISO14443_4_Mode")]
        public static extern DL_STATUS SetISO14443_4_Mode();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "APDUTransceive")]
        public unsafe static extern DL_STATUS APDUTransceive(byte cls, byte ins, byte p1, byte p2, char[] data_out, int Nc, byte[] data_in, int* Ne,
             byte send_le, byte[] apdu_status);


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "APDUTransceive")]
        public unsafe static extern DL_STATUS APDUTransceive_Bytes(byte cls, byte ins, byte p1, byte p2, byte[] data_out, int Nc, byte[] data_in, int* Ne,
             byte send_le, byte[] apdu_status);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "s_block_deselect")]
        public unsafe static extern DL_STATUS s_block_deselect(byte timeout);

        //---------------------------------------------------------------------------------------------------------------------------------

        public EMV_STATUS getSfi(ref emv_tree_node_t tag_node, byte* sfi)
        {
            if (!tag_node.is_node_type)
                return EMV_STATUS.EMV_ERR_TAG_NOT_FOUND;

            if (tag_node.tag == 0x88)
            {
                if (tag_node.value_len == 1)
                {
                    *sfi = (byte)tag_node.value[0];
                    return EMV_STATUS.EMV_OK;
                }
                else
                {
                    return EMV_STATUS.EMV_ERR_TAG_WRONG_SIZE;
                }
            }
            else
            {
                if (tag_node.subnode != null)
                {
                    return getSfi(ref tag_node.subnode, sfi);
                }
                else
                {

                    return getSfi(ref tag_node.next, sfi);
                }
            }


        }

        //--------------------------------------------------------------------

        public unsafe EMV_STATUS getAid(ref emv_tree_node_t tag_node, ref byte[] aid, ref byte aid_len)
        {
            EMV_STATUS status;
            while (tag_node.value_len != 0)
            {
                status = getAid__(ref tag_node, ref aid, ref aid_len);
                if (status == EMV_STATUS.EMV_OK)
                {
                    return EMV_STATUS.EMV_OK;
                }
                tag_node = tag_node.next;
            }

            return EMV_STATUS.EMV_ERR_TAG_NOT_FOUND;
        }

        //--------------------------------------------------------------------

        public unsafe EMV_STATUS getAid__(ref emv_tree_node_t tag_node, ref byte[] aid, ref byte aid_len)
        {
            if (tag_node.value_len == 0)
            {
                return EMV_STATUS.EMV_ERR_TAG_NOT_FOUND;
            }

            if (tag_node.tag == 0x4F)
            {
                if (tag_node.value_len < 17)
                {

                    aid_len = (byte)tag_node.value_len;
                    Array.Copy(tag_node.value, aid, tag_node.value_len);

                    return EMV_STATUS.EMV_OK;
                }
                else
                {
                    return EMV_STATUS.EMV_ERR_TAG_WRONG_SIZE;
                }
            }
            else
            {
                if (tag_node.subnode != null)
                {
                    return getAid__(ref tag_node.subnode, ref aid, ref aid_len);
                }
                else
                {
                    return getAid__(ref tag_node.next, ref aid, ref aid_len);
                }
            }

        }

        //--------------------------------------------------------------------

        public unsafe EMV_STATUS getLogEntry(emv_tree_node_s tag_node, byte* sfi, byte* log_records)
        {
            EMV_STATUS status;

            while (tag_node.value_len > 0)
            {
                status = getLogEntry__(tag_node, sfi, log_records);
                if (status == EMV_STATUS.EMV_OK)
                {
                    return EMV_STATUS.EMV_OK;
                }
                tag_node = tag_node.next;
            }
            return EMV_STATUS.EMV_ERR_TAG_NOT_FOUND;
        }

        //--------------------------------------------------------------------

        public unsafe EMV_STATUS getLogEntry__(emv_tree_node_s tag_node, byte* sfi, byte* log_records)
        {
            if (tag_node.value_len == 0)
            {
                return EMV_STATUS.EMV_ERR_TAG_NOT_FOUND;
            }
            if (tag_node.tag == 0x9F4D)
            {
                if (tag_node.value_len == 2)
                {
                    *sfi = (byte)tag_node.value[0];
                    *log_records = (byte)tag_node.value[1];
                    return EMV_STATUS.EMV_OK;
                }
                else
                {
                    return EMV_STATUS.EMV_ERR_TAG_WRONG_SIZE;
                }
            }
            else
            {
                if (tag_node.subnode != null)
                {
                    return getLogEntry__(tag_node.subnode, sfi, log_records);
                }
                else
                {
                    return getLogEntry__(tag_node.next, sfi, log_records);
                }
            }
        }

        //--------------------------------------------------------------------

        public unsafe EMV_STATUS getListLength(ref emv_tree_node_t tag_node, ushort* length)
        {

            emv_tree_node_t p = new emv_tree_node_t();

            *length = 0;

            if (tag_node.value_len == 0)
                return EMV_STATUS.EMV_ERR_TAG_NOT_FOUND;
            if (tag_node.tag_type != tag_type_t.TL_LIST)
                return EMV_STATUS.EMV_ERR_TAG_WRONG_TYPE;
            if (tag_node.tl_list_format.value_len == 0)
            {
                return EMV_STATUS.EMV_ERR_LIST_FORMAT_NOT_FOUND;
            }

            p = tag_node.tl_list_format;

            while (p != null)
            {
                *length += (ushort)p.value_len;
                p = p.next;
            }
            return EMV_STATUS.EMV_OK;
        }

        //--------------------------------------------------------------------

        public unsafe EMV_STATUS getAfl(emv_tree_node_t tag_node, afl_list_item_t afl_list_item, byte* afl_list_count)
        {
            byte items = 0;
            byte[] value_ptr = new byte[tag_node.value_len];
            afl_list_item_t temp = new afl_list_item_t();
            afl_list_item_t p = new afl_list_item_t();
         
            EMV_STATUS status;

            *afl_list_count = 0;

            if (tag_node.value_len == 0)
            {
                return EMV_STATUS.EMV_ERR_TAG_NOT_FOUND;
            }
            if (tag_node.tag == 0x94)
            {
                if ((tag_node.value_len == 0) || ((tag_node.value_len % 4) > 0)) //first 2 bytes are AIP
                    return EMV_STATUS.EMV_ERR_TAG_WRONG_SIZE;
                else
                {
                    items = (byte)(tag_node.value_len / 4);
                    
                    value_ptr = tag_node.value;


                    byte ptr_val = value_ptr[0];
                    while (items > 0)
                    {
                       
                        status = newAflListItem(ref p);
                        if (afl_list_item == null)
                        {
                            if (status > 0)
                            { 
                                return status;
                            }
                            afl_list_item = p;
                            temp = p;
                        }
                        else
                        {
                            if (status > 0)
                            {
                                emvAflListCleanup(afl_list_item);
                                afl_list_item = null;
                                return status;
                            }
                            afl_list_item.next = p;
                            afl_list_item = afl_list_item.next;
                        }


                        byte* in_ptr;
                        in_ptr = (byte*)Marshal.AllocHGlobal((int)value_ptr.Length);
                        Marshal.Copy(value_ptr, 0, (IntPtr)in_ptr, value_ptr.Length);
                        
                       

                        p.sfi = *in_ptr++;
                            p.sfi >>= 3;
                            p.record_first = *in_ptr++;
                            p.record_last = *in_ptr++;
                            p.record_num_offline_auth = *in_ptr++;


                        Array.Copy(value_ptr, 4, value_ptr, 0,value_ptr.Length - 4);
                        
                            items--;
                    }

                    *afl_list_count = (byte)(tag_node.value_len / 4);
                                  

                    return EMV_STATUS.EMV_OK;

                }

            } else
            {
                if (tag_node.subnode != null)
                {
                    return getAfl( tag_node.subnode, afl_list_item, afl_list_count);
                }
                else
                {
                    return getAfl( tag_node.next,  afl_list_item, afl_list_count);
                }
            }
        }

        //--------------------------------------------------------------------
        
        public unsafe EMV_STATUS getAflFromResponseMessageTemplateFormat1(emv_tree_node_t tag_node, afl_list_item_t afl_list_item, byte* afl_list_count)
        {
            byte items = 0, len = 0;
            byte[] value_ptr = new byte[tag_node.value_len];
            
            bool first_item = true;
            afl_list_item_t temp = new afl_list_item_t();
            afl_list_item_t p = new afl_list_item_t();

            EMV_STATUS status;

            *afl_list_count = 0;

            if (tag_node == null)
                return EMV_STATUS.EMV_ERR_TAG_NOT_FOUND;
            
            if (tag_node.tag == 0x80)
            {
                len = (byte)(tag_node.value_len - 2); // first 2 bytes are AIP
                if ((len==0) || ((len % 4) == 0))
                { // first 2 bytes are AIP
                    return EMV_STATUS.EMV_ERR_TAG_WRONG_SIZE;
                }
                else
                {
                    items = (byte)(len / 4);
                    value_ptr = tag_node.value;  // first 2 bytes are AIP
                    while (items > 0)
                    {
                        status = newAflListItem(ref p); // all members are cleared
                        if (first_item)
                        {
                            if (status > 0)
                                return status;
                            afl_list_item = p;
                            temp = p;
                            first_item = false;
                        }
                        else
                        {
                            if (status > 0)
                            {
                                emvAflListCleanup(afl_list_item);
                                return status;
                            }
                            afl_list_item.next = p;
                            afl_list_item = afl_list_item.next;
                        }


                        byte* in_ptr;
                        in_ptr = (byte*)Marshal.AllocHGlobal((int)value_ptr.Length);
                        Marshal.Copy(value_ptr, 0, (IntPtr)in_ptr, value_ptr.Length);


                        p.sfi = *in_ptr++;
                        p.sfi >>= 3;
                        p.record_first = *in_ptr++;
                        p.record_last = *in_ptr++;
                        p.record_num_offline_auth = *in_ptr++;

                        Array.Copy(value_ptr, 4, value_ptr, 0, value_ptr.Length - 4);

                        items--;
                    }
                    *afl_list_count = (byte)(len / 4);
                    return EMV_STATUS.EMV_OK;
                }
            }
            else
            {
                if (tag_node.subnode != null)
                {
                    return getAfl(tag_node.subnode, afl_list_item, afl_list_count);
                }
                else
                {
                    return getAfl(tag_node.next, afl_list_item, afl_list_count);
                }
            }
        }
        //------------------------------------------------------------------------------

        public EMV_STATUS newAflListItem(ref afl_list_item_t afl_list)
        {
            afl_list_item_t p = new afl_list_item_t();

            if (p == null)
            {
                return EMV_STATUS.SYS_ERR_OUT_OF_MEMORY;
            } else
            {
                afl_list = p;
            }

            p.sfi = 0;
            p.record_first = 0;
            p.record_last = 0;
            p.record_num_offline_auth = 0;
            p.next = null;

            afl_list = p;

            return EMV_STATUS.EMV_OK;
        }

        //--------------------------------------------------------------------

        public void emvAflListCleanup(afl_list_item_s head)
        {
            afl_list_item_s temp;

            while (head.sfi != 0)
            {
                temp = head.next;
                head = temp;
            }
        }

        //--------------------------------------------------------------------

        public unsafe EMV_STATUS parseEmvTag(byte* tag_ptr, emv_tag_t* tag, ref byte[] tag_val, int* tag_len, int* tag_len_len, int* tag_val_len)
        {

            *tag = *tag_ptr++;
            *tag_len = 1;
            if ((*tag & 0x1F) == 0x1F)
            {
                *tag <<= 8;
                *tag |= *tag_ptr;
                (*tag_len)++;
                if ((*tag_ptr++ & 0x80) == 0x80)
                {
                    *tag <<= 8;
                    *tag |= *tag_ptr++;
                    (*tag_len)++;
                }
            }

            //Length
            *tag_len_len = 1;
            *tag_val_len = *tag_ptr;
            if ((*tag_ptr & 0x80) == 0x80)
            {
                *tag_len_len += *tag_ptr & 0x7F;
            }
            if (*tag_len_len > MAX_TAG_LEN_BYTES)
            {
                return EMV_STATUS.EMV_ERR_MAX_TAG_LEN_BYTES_EXCEEDED;
            }

            if (*tag_len_len > 1)
            {
                *tag_val_len = 0;
                for (int i = *tag_len_len - 1; i > 0; i--)
                {
                    *tag_val_len |= *tag_ptr++ << ((i - 1) * 8);
                }
            }

            tag_ptr++;
            tag_val = new byte[*tag_val_len];

            for (int i = 0; i < *tag_val_len; i++)
            {
                tag_val[i] = *tag_ptr;
                tag_ptr++;
            }
            
            return EMV_STATUS.EMV_OK;

        }

        //--------------------------------------------------------------------

        public unsafe EMV_STATUS newEmvTag(ref emv_tree_node_t head, byte[] input, int input_bytes_left, bool is_list_format)
        {
            emv_tree_node_t p = new emv_tree_node_t();
            emv_tag_index_t tag_index = 0;
            emv_tag_t tag = 0;
            byte[] tag_val = null;
            int tag_len = 0;
            int tag_len_len = 0;
            int tag_val_len = 0;
            int temp = 0;
            bool is_node_type;

            EMV_STATUS status;


            fixed (byte* input_ptr = input)
                status = parseEmvTag(input_ptr, &tag, ref tag_val, &tag_len, &tag_len_len, &tag_val_len);

            if (status != EMV_STATUS.EMV_OK)
            {
                return status;
            }

            tag_index = findEmvTagIndex(tag);

            is_node_type = (emv_tags[tag_index].tag_type == tag_type_t.NODE);

            temp = tag_len + tag_len_len;

            if (!is_node_type && !is_list_format)
            {
                temp += tag_val_len;
            }
            input_bytes_left -= temp;

            byte* in_ptr;
            in_ptr = (byte*)Marshal.AllocHGlobal((int)input.Length);
            Marshal.Copy(input, 0, (IntPtr)in_ptr, input.Length);
            in_ptr += (byte)temp;
            Marshal.Copy((IntPtr)in_ptr, input, 0, input.Length);

            if (p == null)
            {
                return EMV_STATUS.SYS_ERR_OUT_OF_MEMORY;
            }

            else
            {
                head = p;
            }

            p.is_node_type = is_node_type;
            p.tag = tag;
            p.tag_bytes = (byte)tag_len;
            p.tag_type = emv_tags[tag_index].tag_type;
            p.description = emv_tags[tag_index].description;
            p.tl_list_format = null;
            p.subnode = null;

            p.value_len = tag_val_len;

            if (!(p.is_node_type) && !is_list_format && (tag_val_len > 0))
            {
                if (p.tag_type == tag_type_t.STR)
                {
                    temp = tag_val_len + 1;
                }

                p.value = tag_val;

                if (p.value == null)
                {
                    return EMV_STATUS.SYS_ERR_OUT_OF_MEMORY;
                }
                
            }

            if (p.tag_type == tag_type_t.TL_LIST)
            {

                status = newEmvTag(ref p.tl_list_format,p.value, p.value_len, true);
            }

            if ((input_bytes_left < 0) || (is_node_type && (input_bytes_left != tag_val_len)))
            {
                return EMV_STATUS.EMV_ERR_WRONG_INPUT_DATA;
            }

            else if (input_bytes_left > 0)
            {
                if (p.is_node_type)
                {
                    status = newEmvTag(ref p.subnode,input, input_bytes_left, false);
                }
                else
                {
                    status = newEmvTag(ref p.next, input, input_bytes_left, is_list_format);
                }

                if (status != EMV_STATUS.EMV_OK)
                {
                    return status;
                }

            }
            
            return EMV_STATUS.EMV_OK;

        }

        //--------------------------------------------------------------------

        public emv_tag_index_t findEmvTagIndex(emv_tag_t tag)
        {
            emv_tag_index_t i = 0;

            do
            {
                if (emv_tags[i].tag == tag)
                    break;
                i++;
            } while (emv_tags[i].tag_id_len != 0);

            return i;
        }

        //--------------------------------------------------------------------

        public EMV_STATUS emvReadRecord(byte[] r_apdu, int* Ne, byte sfi, byte record, byte[] sw)
        {
            DL_STATUS status; //DL_STATUS == UFR_STATUS
           
            sfi <<= 3;
            sfi |= 4;
            ushort* sw16_ptr = (ushort*)BitConverter.ToUInt16(sw, 0);
            *Ne = 256;

            status = APDUTransceive(0x00, 0xB2, record, sfi, null, 0, r_apdu, Ne, 1, sw);
            if (status != 0)
                return EMV_STATUS.EMV_ERR_IN_CARD_READER;

            if (sw[0] == 0x6C)
            {
                *Ne = sw[1];

                status = APDUTransceive(0x00, 0xB2, record, sfi, null, 0, r_apdu, Ne, 1, sw);
                if (status != 0)
                {
                    return EMV_STATUS.EMV_ERR_IN_CARD_READER;
                }
                else if (*sw16_ptr == 0x8262)
                    *sw16_ptr = 0x90;

                if (*sw16_ptr != 0x90)
                    return EMV_STATUS.EMV_ERR_READING_RECORD;

            }
            return EMV_STATUS.EMV_OK;
        }

        //--------------------------------------------------------------------

        public EMV_STATUS formatGetProcessingOptionsDataField(emv_tree_node_t tag_node, ref byte[] gpo_data_field, ushort* gpo_data_field_size)
        {
            byte* temp = null;
            emv_tree_node_t pdol = null, p = null;
            EMV_STATUS status;

            gpo_data_field = null;
            *gpo_data_field_size = 0;

            status = getPdol(ref tag_node, ref pdol);
            if ((status > 0) && status != EMV_STATUS.EMV_ERR_TAG_NOT_FOUND)
                return status;

            if (status == EMV_STATUS.EMV_OK && (pdol == null))
                return EMV_STATUS.EMV_ERR_PDOL_IS_EMPTY;

            if (status != EMV_STATUS.EMV_ERR_TAG_NOT_FOUND)
            {

                p = pdol;
                while (p != null)
                {
                    *gpo_data_field_size += (ushort)p.value_len;
                    p = p.next;
                }

                if (*gpo_data_field_size == 0)
                    return EMV_STATUS.EMV_ERR_PDOL_IS_EMPTY;
            }

            *gpo_data_field_size += 2;

            gpo_data_field = new byte[*gpo_data_field_size];

            if (gpo_data_field == null)
            {
                *gpo_data_field_size = 0;
                return EMV_STATUS.SYS_ERR_OUT_OF_MEMORY;
            }

            (gpo_data_field)[0] = 0x83;
            (gpo_data_field)[1] = (byte)(*gpo_data_field_size - 2);

            if (status != EMV_STATUS.EMV_ERR_TAG_NOT_FOUND)
            {
                p = pdol;
                fixed(byte* gpo_ptr = gpo_data_field)
                temp = gpo_ptr + 2;
                while (p != null)
                {
                    if (p.tag == 0x9F66) // Terminal Transaction Qualifiers (TTQ) Tag
                    {
                        temp[0] = 0x28;
                        //				temp[1] = 0x20;
                        //				temp[2] = 0xC0;
                        //				temp[3] = 0x00;
                    }
                    else if (p.tag == 0x5F2A) //
                    {
                        temp[0] = 0x09;
                        temp[1] = 0x41;
                    }
                    else if (p.tag == 0x9A03)
                    {
                        temp[0] = 0x17;
                        temp[1] = 0x08;
                        temp[2] = 0x15;
                    }

                    temp += p.value_len;
                    p = p.next;
                }
            }

            return EMV_STATUS.EMV_OK;
        }
        //--------------------------------------------------------------------
        public EMV_STATUS getPdol(ref emv_tree_node_t tag_node,ref emv_tree_node_t pdol)
        {
            if (tag_node != null)
                return EMV_STATUS.EMV_ERR_TAG_NOT_FOUND;

            if (tag_node.tag == 0x9f38)
            {
                if (tag_node.value_len > 0)
                {
                    pdol = tag_node.tl_list_format;
                    return EMV_STATUS.EMV_OK;
                }
                else
                {
                    return EMV_STATUS.EMV_ERR_TAG_WRONG_SIZE;
                }
            }
            else
            {
                if (tag_node.subnode != null)
                {
                    return getPdol(ref tag_node.subnode, ref pdol);
                }
                else
                {
                    return getPdol(ref tag_node.next, ref pdol);
                }
            }
        }
        //--------------------------------------------------------------------
        public bool isExistATCounter( emv_tree_node_t log_list_item_format, ushort* pos, ushort* len)
        {
            *pos = 0;
            while (log_list_item_format != null)
            {
                if (log_list_item_format.tag == 0x9f36)
                {
                    *len = (ushort)log_list_item_format.value_len;
                    return true;
                }
                *pos += (ushort)log_list_item_format.value_len;
                log_list_item_format = log_list_item_format.next;
            }
            return false;
        }
        //------------------------------------------------------------------------------
        public bool isExistTransactionDate( emv_tree_node_t log_list_item_format, ushort* pos, ushort* len)
        {

            *pos = 0;

            while (log_list_item_format != null)
            {
                if (log_list_item_format.tag == 0x9a)
                {
                    *len = (ushort)log_list_item_format.value_len;
                    return true;
                }
                *pos += (ushort)log_list_item_format.value_len;
                log_list_item_format = log_list_item_format.next;
            }
            return false;
        }
        //------------------------------------------------------------------------------
        public bool isExistTransactionTime( emv_tree_node_t log_list_item_format, ushort* pos, ushort* len)
        {
            *pos = 0;
            while (log_list_item_format != null)
            {
                if (log_list_item_format.tag == 0x9f21)
                {
                    *len = (ushort)log_list_item_format.value_len;
                    return true;
                }
                *pos += (ushort)log_list_item_format.value_len;
                log_list_item_format = log_list_item_format.next;
            }
            return false;
        }
        //------------------------------------------------------------------------------
        public bool isExistAmountAuthorised( emv_tree_node_t log_list_item_format, ushort* pos, ushort* len)
        {
            *pos = 0;
            while (log_list_item_format != null)
            {
                if (log_list_item_format.tag == 0x9f02)
                {
                    *len = (ushort)log_list_item_format.value_len;
                    return true;
                }
                *pos += (ushort)log_list_item_format.value_len;
                log_list_item_format = log_list_item_format.next;
            }
            return false;
        }
        //------------------------------------------------------------------------------
        public bool isExistTransactionCurrency( emv_tree_node_t log_list_item_format, ushort* pos, ushort* len)
        {
            *pos = 0;
            while (log_list_item_format != null)
            {
                if (log_list_item_format.tag == 0x5f2a)
                {
                    *len = (ushort)log_list_item_format.value_len;
                    return true;
                }
                *pos += (ushort)log_list_item_format.value_len;
                log_list_item_format = log_list_item_format.next;
            }
            return false;
        }
        //------------------------------------------------------------------------------
        public bool isExistTerminalCountry(emv_tree_node_t log_list_item_format, ushort* pos, ushort* len)
        {
            *pos = 0;
            while (log_list_item_format != null)
            {
                if (log_list_item_format.tag == 0x9f1a)
                {
                    *len = (ushort)log_list_item_format.value_len;
                    return true;
                }
                *pos += (ushort)log_list_item_format.value_len;
                log_list_item_format = log_list_item_format.next;
            }
            return false;
        }
        //--------------------------------------------------------------------
         public UInt64 bin_bcd_to_ll( byte* bin, int len)
        {
            UInt64 result = 0;
            UInt64 dec = 1;

            for (int i = len; i > 0; i--)
            {
                result += (ulong)(bin[i - 1] & 0x0F) * dec;
                 dec *= 10;
                result += (ulong)(bin[i - 1] >> 4) * dec;
                dec *= 10;
            }

            return result;


        }
        //------------------------------------------------------------------------------
       public string findCurrencyIndexByNumCode(ushort num_code)
        {
            int i = 0;

            do
            {
                if (iso4217_currency_codes[i].num_code == num_code)
                    break;
                i++;
            } while (iso4217_currency_codes[i].num_code != 0);

            return iso4217_currency_codes[i].alpha_code;
        }

        //---------------------------------------------------------------------------------------------

        public bool GetCardNumber(byte[] value, int val_len, ref byte[] card_nr,int* card_nr_len)
        {
            bool number_found = false;
            
          for (int i = 0; i < val_len; i++)
            {
                if (value[i] == 0x5A)
                {
                    i = i+1;
                    *card_nr_len = value[i];
                    i = i+1;
                    card_nr = new byte[*card_nr_len];
                    for (int nr = 0; nr < *card_nr_len; nr++)
                    {
                        card_nr[nr] = value[nr+i];
                    }
                    number_found = true;
                    break;
                }

                number_found = false;
            }

            return number_found;
        }

        public static byte[] ToByteArray(String HexString)
        {

            int NumberChars = HexString.Length;
            byte[] bytes = new byte[NumberChars / 2];

            if (HexString.Length % 2 != 0)
            {
                return bytes;
            }

            for (int i = 0; i < NumberChars; i += 2)
            {
                try
                {
                    bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
                }
                catch (System.FormatException)
                {
                    System.Windows.Forms.MessageBox.Show("Incorrect format!");
                    break;
                }
            }

            return bytes;
        }
    }

  }


/**********************************************************************************
 * Multi Threaded Webserver implementation
 *  
 * A fully threaded webserver implementation. 
 * 
 * Wait's for connections in it's own thread spawning new threads for recieved connections
 * Automaticly frees the finished worker threads.
 * 
 * (C)opyright Elze Kool, 2008, http://www.microframework.nl
 * UrlDecode function (C)opyright Jan Kucera http://informatix.miloush.net/microframework/
 * 
 * This code is provided AS-IS. I take no responsibility for any damage that occours when using this class
 * or any part of it. You may use this code freely non-commericaly and commercialy as long as you keep
 * in above copyright notice.
 * 
 **********************************************************************************/

// Base 
using System;
using Microsoft.SPOT;

// Used for Encoding/Decoding UTF8
using System.Text;

// Used for ArrayList
using System.Collections;

namespace ElzeKool.net
{
    /// <summary>
    /// Storage for GET or POST parameters.
    /// Used for passing parameters to the request handler
    /// 
    /// Use static RequestParameter.Get function to retrieve values from
    /// GET or POST array. Can URL decode the value
    /// </summary>
    public class RequestParameter
    {
        /// <summary>
        /// Field name
        /// Better to use static RequestParameter.Get function to get values!
        /// </summary>
        public String Field = "";

        /// <summary>
        /// Field value
        /// Better to use static RequestParameter.Get function to get values!
        /// </summary>
        public String Value = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public RequestParameter(String Field, String Value)
        {
            this.Field = Field;
            this.Value = Value;
        }

        #region Static functions

        /// <summary>
        /// URL Decode string
        /// From: 
        /// Jan Kucera
        /// http://informatix.miloush.net/
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static String UrlDecode(String s)        
        {            
            if (s == null) return null;            
            
            if (s.Length < 1) return s;             
            char[] chars = s.ToCharArray();            
            byte[] bytes = new byte[chars.Length * 2];            
            int count = chars.Length;            
            int dstIndex = 0;            
            int srcIndex = 0;             
            while (true)            
            {                
                if (srcIndex >= count)                
                {                    
                    if (dstIndex < srcIndex)                    
                    {                        
                        byte[] sizedBytes = new byte[dstIndex];                        
                        Array.Copy(bytes, 0, sizedBytes, 0, dstIndex);                        
                        bytes = sizedBytes;                    
                    }                    
                    
                    return new string(Encoding.UTF8.GetChars(bytes));                
                }                 
                if (chars[srcIndex] == '+')                
                {                    
                    bytes[dstIndex++] = (byte)' ';                    
                    srcIndex += 1;                
                }                
                else if (chars[srcIndex] == '%' && srcIndex < count - 2)                    
                    if (chars[srcIndex + 1] == 'u' && srcIndex < count - 5)                    
                    {                        
                        int ch1 = HexToInt(chars[srcIndex + 2]);                        
                        int ch2 = HexToInt(chars[srcIndex + 3]);                        
                        int ch3 = HexToInt(chars[srcIndex + 4]);                        
                        int ch4 = HexToInt(chars[srcIndex + 5]);                         
                        if (ch1 >= 0 && ch2 >= 0 && ch3 >= 0 && ch4 >= 0)                        
                        {                            
                            bytes[dstIndex++] = (byte)((ch1 << 4) | ch2);                            
                            bytes[dstIndex++] = (byte)((ch3 << 4) | ch4);                            
                            srcIndex += 6;                            
                            continue;                        
                        }                    
                    }                    
                    else                    
                    {                        
                        int ch1 = HexToInt(chars[srcIndex + 1]);                        
                        int ch2 = HexToInt(chars[srcIndex + 2]);                         
                        if (ch1 >= 0 && ch2 >= 0)                        
                        {                            
                            bytes[dstIndex++] = (byte)((ch1 << 4) | ch2);                            
                            srcIndex += 3;                            
                            continue;                        
                        }                    
                    }                
                else                
                {                    
                    byte[] charBytes = Encoding.UTF8.GetBytes(chars[srcIndex++].ToString());                    
                    charBytes.CopyTo(bytes, dstIndex);                    
                    dstIndex += charBytes.Length;                
                }            
            }        
        }         
        
        /// <summary>
        /// Return number from Hex char (0-F => 0-15)
        /// Used in URLDecode
        /// </summary>
        /// <param name="ch">Char to convert</param>
        /// <returns>Converted char value, -1 if invalid</returns>
        private static int HexToInt(char ch)        
        {            
            return 
                (ch >= '0' && ch <= '9') ? ch - '0' :  
                (ch >= 'a' && ch <= 'f') ? ch - 'a' + 10 :                
                (ch >= 'A' && ch <= 'F') ? ch - 'A' + 10 :  -1;        
        }
        
        /// <summary>
        /// Get UrlDecoded value from RequestParameter array.
        /// </summary>
        /// <param name="RequestParameters">RequestParameter array</param>
        /// <param name="Field">Field to get</param>
        /// <returns>Value of field, null if not found</returns>
        public static String Get(ref RequestParameter[] RequestParameters, String Field)
        {
            return Get(ref RequestParameters, Field, true);
        }

        /// <summary>
        /// Get value from RequestParameter array.
        /// </summary>
        /// <param name="RequestParameters">RequestParameter array</param>
        /// <param name="Field">Field to get</param>
        /// <param name="DoURLDecode">Indicate if we should UrlDecode the value</param>
        /// <returns>Value of field, null if not found</returns>
        public static String Get(ref RequestParameter[] RequestParameters, String Field, bool DoURLDecode)
        {
            // If null return null
            if (RequestParameters == null) return null;

            // Start with null
            String retString = null;

            // Find
            foreach (RequestParameter r in RequestParameters)
            {
                if (r.Field == Field) retString = r.Value;
            }

            if (DoURLDecode == true)
                return UrlDecode(retString);
            else
                return retString;
        }
        
        /// <summary>
        /// Create RequestParameter array from string in URL(GET) or Request body(POST)
        /// </summary>
        /// <param name="Request">Request string</param>
        /// <returns>Request array or null if empty string</returns>
        public static RequestParameter[] CreateFromRequest(String Request)
        {
            // If empty string return null
            if (Request == "") return null;

            // Explode on & 
            String[] ExplodedRequest = Request.Split('&');

            // Make array
            RequestParameter[] retRequest = new RequestParameter[ExplodedRequest.Length];

            // Go trough array
            for (int x =0; x < ExplodedRequest.Length; x++)
            {
                // Split request part
                String[] RequestPart = ExplodedRequest[x].Split('=');

                String Field = RequestPart[0];
                String Value = "";
                if (RequestPart.Length >= 2)
                    Value = RequestPart[1];

                // Add to array
                retRequest[x] = new RequestParameter(Field, Value);
            }

            // Return the array
            return retRequest;
        }

        #endregion
    }
}

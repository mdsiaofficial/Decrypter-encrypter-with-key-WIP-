using System;
using System.Text.RegularExpressions;
/*
    -You entering key for crypting
    -Program uses this key to encrypt and to decrypt "input_text"
*/
namespace SoloLearn
{
    class Program
    {
        static void Main()
        {
            string input_text = "~99.99% sample Text!", coded_text, decoded_text;
            string entered_key = Console.ReadLine();
            
            // double coding 
            coded_text = encrypting(entered_key, input_text);          
            coded_text = encrypting(entered_key, coded_text);          
            Console.WriteLine("Encrypted  text: " + coded_text);
            // double decoding 
            decoded_text = decrypting(coded_text);
            decoded_text = decrypting(decoded_text);
            Console.WriteLine("Decrypted text: " + decoded_text);
        }
        
        
        static string encrypting(string entered_key, string text)
        {
            Random rnd = new Random();
            string encrypted_text = "";
            char ch;
            int key = 0;
            // processing "entered_key"
            for(int i =0;i<entered_key.Length;i++)   
            {
                ch = Convert.ToChar(entered_key.Substring(i,1)); 
                key += (Byte)ch;    
            }
            key%=94;
            int bufferkey = key/10+key%10*10;
            // coding "text"
            for(int i=0;i<text.Length;i++)
            {
            if(key + 3 > 93)
                key -= 84;
            else
                key += 9;
                
                ch = Convert.ToChar(text.Substring(i,1));
                    if((int)ch+key>126)
                        ch = Convert.ToChar((int)ch+key-96);
                    else
                        ch = Convert.ToChar((int)ch+key);
                encrypted_text += ch;
            }
            if(rnd.Next(2) == 0)
            return bufferkey.ToString() + "." + encrypted_text;
            else 
            return encrypted_text + "." + bufferkey;
        }
        
        
        static string decrypting(string coded_text)
        {
            string decrypted_text = "";
            char ch;
            int key;
            if(coded_text.IndexOf(".")<=2)
            {
            key = int.Parse(Regex.Match(coded_text, @"^\d*").ToString());
            coded_text = Regex.Replace(coded_text, @"^\d+\.", "");
            }
            else 
            {
            key = int.Parse(Regex.Match(coded_text, @"\d*$").ToString());
            coded_text = Regex.Replace(coded_text, @"\.\d+$", "");
            }
            key = key/10+key%10*10;
            if(key<10)
            key *= 10;

            // decoding "coded_text"
            for(int i=0;i<coded_text.Length;i++)
            {
            if(key + 3 > 93)
                key -= 84;
            else
                key += 9;
                
                ch = Convert.ToChar(coded_text.Substring(i,1));
                    if((int)ch-key<32)
                        ch = Convert.ToChar((int)ch-key+96);
                    else
                        ch = Convert.ToChar((int)ch-key);
                decrypted_text += ch;
            }
                return decrypted_text;
        }                    
    }
}

using System;
using System.IO;
using System.Text;

class simple_encrypt_main
{
    static void Main()
    {
        double version = 0.85;
        Console.Title = ($"SimpleEncrypt - V {version}");
        Console.WriteLine($"===Simple Encrypt===\n--Version: {version}--\n--Made by: Rim032--\n");

        string[] main_args = new string[3];
        bool ed_input_is_file = false;


        Console.WriteLine("Enter -e (encrypt) or -d (decrypt).");
        main_args[0] = Convert.ToString(Console.ReadLine());

        Console.WriteLine("Enter a string or drag and drop a file.");
        main_args[1] = Convert.ToString(Console.ReadLine());

        Console.WriteLine("Enter a unique key for encryption and decryption.");
        main_args[2] = Convert.ToString(Console.ReadLine());

        if (File.Exists(format_file_location(main_args[1])))
        {
            ed_input_is_file = true;
        }

        string result = "null";
        if (main_args[0].ToLower() == "-e")
        {
            result = decrypt_encrypt(main_args[1], main_args[2], ed_input_is_file, true);
        }
        else
        {
            result = decrypt_encrypt(main_args[1], main_args[2], ed_input_is_file, false);
        }

        Console.WriteLine($"PEA32 Result: \"{result}\"");

        Console.WriteLine("\nPEA 32 finished... Press any key to exit.");
        Console.ReadKey();
    }

    public static string decrypt_encrypt(string input, string key, bool is_file, bool operation_type)
    {
        if (input == null)
        {
            return "[ERROR]: Encryption input is null/invalid.";
        }

        if (key.Length > input.Length)
        {
            return "[ERROR]: Encryption key is too long.";
        }


        int[] operation_nums = new int[3] { 2, -2, -1 };
        Byte[] key_bytes = Encoding.ASCII.GetBytes(key);
        Byte[] input_bytes = Encoding.ASCII.GetBytes(input);

        if(!operation_type)
        {
            for(int n = 0; n < operation_nums.Length; n++)
            {
                operation_nums[n] = operation_nums[n] * -1;
            }
        }

        if (is_file)
        {
            input_bytes = Encoding.ASCII.GetBytes(File.ReadAllText(format_file_location(input)));
        }
        input_bytes.Reverse();
        key_bytes.Reverse();

        string encryption_result = "";

        int k = 0;
        int temp_key_num = 0;

        try
        {
            for (int i = 0; i < input_bytes.Length; i++)
            {
                if (k < key_bytes.Length && i != 0)
                {
                    temp_key_num = Convert.ToUInt16(key_bytes[k]);
                    k++;
                }

                if (input_bytes[i] != 32)
                {
                    if (temp_key_num > 111 && temp_key_num < 125)
                    {
                        input_bytes[i] = Convert.ToByte(input_bytes[i] + operation_nums[0]);
                    }
                    else if (temp_key_num < 111 && temp_key_num > 79)
                    {
                        input_bytes[i] = Convert.ToByte(input_bytes[i] + operation_nums[1]);
                    }
                    else
                    {
                        input_bytes[i] = Convert.ToByte(input_bytes[i] + operation_nums[2]);
                    }
                }

                encryption_result += Convert.ToChar(input_bytes[i]);
            }
        }
        catch(Exception error)
        {
            return ("ERROR: " + error.Message);
        }

        return encryption_result;
    }

    internal static string format_file_location(string file)
    {
        string final_file = "";
        if (file == null)
        {
            return final_file;
        }

        string[] file_arr = file.Split("\"");
        for (int i = 0; i < file_arr.Length; i++)
        {
            if (file_arr[i] != "\"")
            {
                final_file += file_arr[i];
            }
        }

        return final_file;
    }
}

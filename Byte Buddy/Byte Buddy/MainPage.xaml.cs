using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Byte_Buddy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        enum cTypes
        {
            BYTE,
            BYTE_2,
            BYTE_4,
            BYTE_8,
            FLOAT,
            DOUBLE,
            BYTE_ARRAY
        }

        private Dictionary<string, cTypes> lookup = new Dictionary<string, cTypes>();

        public MainPage()
        {
            this.InitializeComponent();

            CB_Types.Items.Add("Byte");
            CB_Types.Items.Add("2 Byte");
            CB_Types.Items.Add("4 Byte");
            CB_Types.Items.Add("8 Byte");
            CB_Types.Items.Add("Float");
            CB_Types.Items.Add("Double");
            CB_Types.Items.Add("Byte Array");


            lookup.Add("Byte", cTypes.BYTE);
            lookup.Add("2 Byte", cTypes.BYTE_2);
            lookup.Add("4 Byte", cTypes.BYTE_4);
            lookup.Add("8 Byte", cTypes.BYTE_8);
            lookup.Add("Float", cTypes.FLOAT);
            lookup.Add("Double", cTypes.DOUBLE);
            lookup.Add("Byte Array", cTypes.BYTE_ARRAY);
        }

        private void Btn_Convert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Console.WriteLine(CB_Types.Text);
                Txt_TextBlock.Text = "";
                switch (lookup[CB_Types.SelectionBoxItem.ToString()])
                {
                    case cTypes.BYTE: {
                            if (byte.TryParse(Txt_Input.Text, out byte ret))
                            {

                                Txt_TextBlock.Text = $"{ret:X2}";
                            }
                            else
                            {
                                Txt_TextBlock.Text = $"Please enter a value between 0-{byte.MaxValue}";
                            }
                        } break;
                    case cTypes.BYTE_2: {
                            if (ushort.TryParse(Txt_Input.Text, out ushort ret))
                            {
                                byte[] bytes = BitConverter.GetBytes(ret);
                                Txt_TextBlock.Text = $"{bytes[0]:X2} {bytes[1]:X2}";
                            }
                            else
                            {
                                Txt_TextBlock.Text = $"Please enter a value between 0-{ushort.MaxValue}";
                            }
                        } break;
                    case cTypes.BYTE_4: {
                            if (uint.TryParse(Txt_Input.Text, out uint ret))
                            {
                                byte[] bytes = BitConverter.GetBytes(ret);
                                Txt_TextBlock.Text = $"{bytes[0]:X2} {bytes[1]:X2} {bytes[2]:X2} {bytes[3]:X2}";
                            }
                            else
                            {
                                Txt_TextBlock.Text = $"Please enter a value between 0-{uint.MaxValue}";
                            }
                        } break;
                    case cTypes.BYTE_8: {
                            if (UInt64.TryParse(Txt_Input.Text, out UInt64 ret))
                            {
                                byte[] bytes = BitConverter.GetBytes(ret);
                                Txt_TextBlock.Text = $"{bytes[0]:X2} {bytes[1]:X2} {bytes[2]:X2} {bytes[3]:X2} {bytes[4]:X2} {bytes[5]:X2} {bytes[6]:X2} {bytes[7]:X2}";
                            }
                            else
                            {
                                Txt_TextBlock.Text = $"Please enter a value between 0-{UInt64.MaxValue}";
                            }
                        } break;
                    case cTypes.FLOAT:
                        {
                            if (float.TryParse(Txt_Input.Text, out float ret))
                            {
                                byte[] bytes = BitConverter.GetBytes(ret);
                                Txt_TextBlock.Text = $"{bytes[0]:X2} {bytes[1]:X2} {bytes[2]:X2} {bytes[3]:X2}";
                            }
                            else
                            {
                                Txt_TextBlock.Text = $"Please enter a value between 0-{float.MaxValue}";
                            }
                        }
                        break;
                    case cTypes.DOUBLE:
                        {
                            if (double.TryParse(Txt_Input.Text, out double ret))
                            {
                                byte[] bytes = BitConverter.GetBytes(ret);
                                Txt_TextBlock.Text = $"{bytes[0]:X2} {bytes[1]:X2} {bytes[2]:X2} {bytes[3]:X2} {bytes[4]:X2} {bytes[5]:X2} {bytes[6]:X2} {bytes[7]:X2}";
                            }
                            else
                            {
                                Txt_TextBlock.Text = $"Please enter a value between 0-{double.MaxValue}";
                            }
                        }
                        break;
                    case cTypes.BYTE_ARRAY:
                        {
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Txt_Input.Text);

                            string tempstr = "";

                            for (int i = 0;i < bytes.Length;i++)
                            {
                                tempstr += $"0x{bytes[i]:X2} ";
                            }
                            Txt_TextBlock.Text = tempstr;
                            
                        }
                        break;
                    default:
                        Txt_TextBlock.Text = "Unknown issue";
                        break;
                }
            }
            catch (Exception ex)
            {
                Txt_TextBlock.Text = "Please select a type!";
            }
        }

        private void Btn_CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dp = new DataPackage();
            dp.SetText(Txt_TextBlock.Text);
            Clipboard.SetContent(dp);
        }
    }
}

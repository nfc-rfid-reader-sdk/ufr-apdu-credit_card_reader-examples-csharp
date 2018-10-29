using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uFrAdvance;
using System.Runtime.InteropServices;
namespace C_sharp_Credit_Card_Reader
{

    using DL_STATUS = System.UInt32;

    public partial class FormMain : Form
    {

        DL_STATUS status;
        public FormMain()
        {
            InitializeComponent();
        }




        unsafe int tryEmvPseLog(string df_name, string szTitlePse)
        {

            

            uFCoder reader = new uFCoder();

            EMV_STATUS emv_status = 0;

            emv_tree_node_t head = new emv_tree_node_t();
            emv_tree_node_t temp = new emv_tree_node_t();
            emv_tree_node_t tail = new emv_tree_node_t();
            emv_tree_node_t log_list_item = new emv_tree_node_t();

            ushort log_list_len = 0;

            bool head_attached = false;

            byte log_records = 0;

            byte[] r_apdu = new byte[258];

            int Ne = 256;

            byte[] sw = new byte[2];

            byte record = 1;

            byte cnt = 1;

            byte aid_len = 0;

            char[] chr_name = new char[df_name.Length];

            byte sfi = 0;

            byte[] log_list_data = null;

            byte[] log_list_ptr = null;
            byte[] aid = new byte[16];
            char[] chr_aid = new char[16];

          

            int len = df_name.Length;

           
            do
            {
                status = uFCoder.SetISO14443_4_Mode();
                if (status != 0)
                {
                    tDebug.AppendText("Error while switching into ISO 14443-4 mode, uFR status is: " + uFCoder.status2str(status) + "\n");
                    break;
                }
                tDebug.AppendText(cnt++.ToString() + ". Issuing Select PSE command: " + df_name + "\n" + "  [C] 00 A4 04 00 " + df_name.Length.ToString("X2") + " ");
                tDebug.AppendText(BitConverter.ToString(Encoding.ASCII.GetBytes(df_name)).Replace("-", " ") + " 00");

                

                char[] name_array = df_name.ToCharArray();

                status = uFCoder.APDUTransceive(0x00, 0xA4, 0x04, 0x00, df_name.ToCharArray(), df_name.Length, r_apdu, &Ne, 1, sw);

                if (status > 0)
                {
                    MessageBox.Show("Error while executing APDU command, uFR status: " + uFCoder.status2str(status));
                    break;
                }
                else
                {
                    if (sw[0] != 0x90)
                    {
                        tDebug.AppendText(" [SW] " + sw[0].ToString("X2") + sw[1].ToString("X2"));
                        tDebug.AppendText(" Could not continue execution due to an APDU error.\n");
                    }

                    if (Ne > 0)
                    {
                        tDebug.Text += "\nAPDU command executed: response data length = " + Ne.ToString() + "\n";

                        tDebug.AppendText(" [R] ");
                        
                        for (int k = 0; k < Ne; k++)
                        {

                            tDebug.AppendText(r_apdu[k].ToString("X2") + ":");

                        }
                     

                    }
          
                    tDebug.AppendText("\n [SW] " + sw[0].ToString("X2") + sw[1].ToString("X2"));
                }
                emv_status = reader.newEmvTag(ref head, r_apdu, Ne, false);

                if (emv_status > 0)
                {
                    tDebug.AppendText("Emv parsing error code: " + emv_status.ToString());
                    break;
                }

                emv_status = reader.getSfi(ref head, &sfi);

                if (emv_status == 0)
                {
                    record = 1;

                    do
                    {
                        tDebug.AppendText("\n" + cnt.ToString() + ". Issuing \"Read Record\" command (record = " + record.ToString() + ", sfi = " + sfi.ToString("X2") + "):\n");
                        tDebug.AppendText("  [C] 00 B2 " + record.ToString("X2") + " " + ((sfi << 3) | 4).ToString("X2"));

                        emv_status = reader.emvReadRecord(r_apdu, &Ne, sfi, record, sw);
                        if (emv_status == 0)
                        {
                            emv_status = reader.newEmvTag(ref temp, r_apdu, Ne, false);

                            if (!head_attached)
                            {
                                head.next = tail = temp;
                                head_attached = true;
                            }
                            else
                            {
                                tail.next = temp;
                                tail = tail.next;
                            }

                            if (Ne > 0)
                            {
                                tDebug.AppendText("\n APDU command executed, response data length: " + Ne.ToString() + "\n");
                                tDebug.AppendText(" [R] ");
                                for (int resp = 0; resp < Ne; resp++)
                                {
                                    tDebug.AppendText(r_apdu[resp].ToString("X2") + ":");
                                }
                            }
                            tDebug.AppendText("\n[SW]: " + sw[0].ToString("X2") + " " + sw[1].ToString("X2"));
                        }
                        else
                        {
                            if (sw[0] != 0x90)
                            {
                                tDebug.AppendText("\n[SW]: " + sw[0].ToString("X2") + " " + sw[1].ToString("X2"));
                                tDebug.AppendText("\nThere is no records");
                            }
                        }
                        record++;
                        cnt++;
                    } while (emv_status == 0);
                }


                ///////////////////////////////////////////////////////////////////////////////////////
               

                emv_status = reader.getAid(ref head, ref aid, ref aid_len);
                if (emv_status == 0)
                {
                    tDebug.AppendText("\n" + cnt++.ToString() + ". Issuing Select the application command: ");

                    tDebug.AppendText("\n[C] 00 A4 04 00 " + aid_len.ToString("X2") + " ");

                    for (int print = 0; print < aid_len; print++)
                    {
                        tDebug.AppendText(aid[print].ToString("X2") + " ");
                    }

                    tDebug.AppendText("00");

                    Ne = 256;

                    Array.Copy(aid, chr_aid, aid.Length);

                    status = uFCoder.APDUTransceive(0x00, 0xA4, 0x04, 0x00, chr_aid, aid_len, r_apdu, &Ne, 1, sw);

                    if (status > 0)
                    {
                        MessageBox.Show("Error while executing APDU command, uFR status is:" + uFCoder.status2str(status));

                        break;
                    }
                    else
                    {
                        if (sw[0] != 0x90)
                        {
                            tDebug.AppendText("[SW]: " + sw[0].ToString("X2") + sw[1].ToString("X2"));

                            break;
                        }

                        if (Ne > 0)
                        {
                            tDebug.AppendText("\nAPDU command executed, response data length: " + Ne.ToString());

                            tDebug.AppendText("\n[R]: ");

                            for (int resp = 0; resp < Ne; resp++)
                            {
                                tDebug.AppendText(r_apdu[resp].ToString("X2") + ":");
                            }
                            tDebug.AppendText("\n[SW]: " + sw[0].ToString("X2") + sw[1].ToString("X2"));
                        }
                    }

                    emv_status = reader.newEmvTag(ref temp, r_apdu, Ne, false);

                    if (emv_status > 0)
                    {
                        MessageBox.Show("EMV parsing error code: " + emv_status.ToString());
                        break;
                    }

                    if (!head_attached)
                    {
                        head.next = tail = temp;
                        head_attached = true;
                    }
                    else
                    {
                        tail.next = temp;
                        tail = tail.next;
                    }
                }

                emv_status = reader.getLogEntry(temp, &sfi, &log_records);
                if (emv_status > 0)
                {
                    tDebug.AppendText("\nCan't find Log Entry. \n");

                    tDebug.AppendText("EMV parsing error code: " + emv_status.ToString());
                }
                else
                {
                    tDebug.AppendText("\n" + cnt++.ToString() + ". Issuing \"Get Log Format\" command:\n");

                    tDebug.AppendText("  [C] 80 CA 9F 4F 00\n");

                    Ne = 256;

                    status = uFCoder.APDUTransceive(0x80, 0xCA, 0x9F, 0x4f, null, 0, r_apdu, &Ne, 1, sw);

                    if (status > 0)
                    {
                        tDebug.AppendText("Error while executing APDU command, status: " + uFCoder.status2str(status));

                        break;
                    }
                    else
                    {
                        if (sw[0] != 0x90)
                        {
                            tDebug.AppendText("[SW]: " + sw[0].ToString("X2") + " " + sw[1].ToString("X2"));

                            tDebug.AppendText(" Could not continue execution due to an APDU error.\n");
                            break;
                        }

                        if (Ne > 0)
                        {
                            tDebug.AppendText("APDU command executed: response data length = " + Ne.ToString() + "\n");

                            for (int j = 0; j < Ne; j++)
                            {
                                tDebug.AppendText(r_apdu[j].ToString("X2") + ":");
                            }
                        }

                        tDebug.AppendText("\n[SW]: " + sw[0].ToString("X2") + sw[1].ToString("X2"));
                    }

                    emv_status = reader.newEmvTag(ref temp, r_apdu, Ne, false);

                    if (emv_status > 0)
                    {
                        tDebug.AppendText("EMV parsing error code: " + emv_status.ToString());

                        break;
                    }

                    tail.next = temp;

                    tail = tail.next;

                    // Now temp->tl_list_format should contain Log List:

                    log_list_item = temp.tl_list_format;

                    if (log_list_item == null)
                    {
                        tDebug.AppendText("\nCan't find Log Entry\n");

                        break;
                    }

                    emv_status = reader.getListLength(ref temp, &log_list_len);

                    if (emv_status > 0)
                    {
                        tDebug.AppendText("\nCan't find Log Entry\n");

                        break;
                    }

                    log_list_data = new byte[log_list_len * log_records];

                    int array_index = 0;

                    for (record = 1; record < log_records + 1; record++)
                    {
                        tDebug.AppendText("\n" + cnt.ToString() + ". Issuing \"Read Record\" command (record = " + record.ToString() + ", sfi = " + sfi.ToString() + "):\n");

                        tDebug.AppendText("[C] 00 B2 " + record.ToString("X2") + " " + ((sfi << 3) | 4).ToString("X2") + " 00");

                        emv_status = reader.emvReadRecord(r_apdu, &Ne, sfi, record, sw);

                        if (emv_status == 0)
                        {

                            if (Ne > 0)
                            {
                                Array.Copy(r_apdu, 0, log_list_data, array_index, Ne);

                                tDebug.AppendText("\n APDU command executed: response data length: " + Ne.ToString() + "\n");

                                for (int j = 0; j < Ne; j++)
                                {
                                    tDebug.AppendText(r_apdu[j].ToString("X2") + ":");
                                }
                                array_index += Ne;
                            }
                            tDebug.AppendText("\n" + "[SW]: " + sw[0].ToString("X2") + " " + sw[1].ToString("X2") + "\n");
                        }
                        else
                        {
                            if (sw[0] != 0x90)
                            {
                                tDebug.AppendText("\n" + "[SW]: " + sw[0].ToString("X2") + " " + sw[1].ToString("X2") + "\n");

                                tDebug.AppendText("there is no records");
                            }
                        }
                        cnt++;
                    }

                }

            } while (false);
            
            var ATCounter = 0;

            ushort ATCounter_pos, TransactionDate_pos, TransactionTime_pos, AmountAuthorised_pos, TransactionCurrency_pos;

            ushort ATCounter_len, TransactionDate_len, TransactionTime_len, AmountAuthorised_len, TransactionCurrency_len;

            String TransactionDate_str = "";

            String TransactionTime_str = "";

            UInt16 short_curr = 0;

            String vrednost = "";

            bool ATCounter_exist = reader.isExistATCounter(log_list_item, &ATCounter_pos, &ATCounter_len);

            bool TransactionDate_exist = reader.isExistTransactionDate(log_list_item, &TransactionDate_pos, &TransactionDate_len);

            bool TransactionTime_exist = reader.isExistTransactionTime(log_list_item, &TransactionTime_pos, &TransactionTime_len);

            bool AmountAuthorised_exist = reader.isExistAmountAuthorised(log_list_item, &AmountAuthorised_pos, &AmountAuthorised_len);

            bool TransactionCurrency_exist = reader.isExistTransactionCurrency(log_list_item, &TransactionCurrency_pos, &TransactionCurrency_len);

            
            string curr = "";

            if (log_list_data != null)
            {
                
                log_list_ptr = log_list_data;
                
                for (int i = 0; i < log_records; i++)
                {
                    if (ATCounter_exist)
                    {
                        if (ATCounter_len == 2)
                        {
                            ATCounter = (ushort)((log_list_ptr[ATCounter_pos]) << 8) + log_list_ptr[ATCounter_pos + 1];
                        }
                    }
                    if (TransactionDate_exist)
                    {
                        TransactionDate_str = log_list_ptr[TransactionDate_pos + 2].ToString("X2") + "." + log_list_ptr[TransactionDate_pos + 1].ToString("X2") + ".20" + log_list_ptr[TransactionDate_pos].ToString("X2");

                    }

                    if (TransactionTime_exist)
                    {
                        TransactionTime_str = log_list_ptr[TransactionTime_pos].ToString("X2") + ":" + log_list_ptr[TransactionTime_pos + 1].ToString("X2") + ":" + log_list_ptr[TransactionTime_pos + 2].ToString("X2");

                    }

                    if (AmountAuthorised_exist)
                    {
                        byte[] ammount = new byte[256];

                        vrednost = "";

                        Array.Copy(log_list_data, AmountAuthorised_pos, ammount, 0, AmountAuthorised_len);

                        String nesto = BitConverter.ToString(ammount).Replace("-", "");
                        
                        int position = 0;

                        for (int z = 2; z < 12 && nesto[z] == '0'; z++)
                        {
                            position = z;
                        }

                        if (nesto[position + 1] != '0')
                        {
                            for (int j = position + 1; j < 12; j++)
                            {
                                vrednost += nesto[j];
                            }
                        }
                        else if (nesto[position] == '0')
                        {
                            vrednost += "0";
                        }

                        try
                        {
                            vrednost = vrednost.Insert(vrednost.Length - 2, ".");
                        }
                        catch (System.ArgumentOutOfRangeException)
                        {
                            vrednost = "0.00";
                        }

                    }

                    if (TransactionCurrency_exist)
                    {
                        short_curr = 0;

                        curr = log_list_ptr[TransactionCurrency_pos].ToString() + log_list_ptr[TransactionCurrency_pos + 1].ToString("X2");

                        short_curr = UInt16.Parse(curr);

                    }
                    
                    dLogs.Rows.Add(ATCounter.ToString(), TransactionDate_str, TransactionTime_str, vrednost, reader.findCurrencyIndexByNumCode(short_curr).ToString());

                    Array.Copy(log_list_data, log_list_len, log_list_ptr, 0, (log_list_data.Length - log_list_len));

                }
                
            }

            MessageBox.Show("Transactions command log printed out in \"Read and parse cards EMV log\" tab.");

            uFCoder.s_block_deselect(100);

            return 0;
        }


        private void bReaderOpen_Click(object sender, EventArgs e)
        {
            status = uFCoder.ReaderOpen();

            if (status == 0)
            {
                uFCoder.ReaderUISignal(1, 1);
            }
            else
            {
                MessageBox.Show(uFCoder.status2str(status));
            }

            tDLLVersion.Text = uFCoder.GetLibraryVersion();
        }

        private void bReaderReset_Click(object sender, EventArgs e)
        {
            status = uFCoder.ReaderReset();

            if (status == 0)
            {
                uFCoder.ReaderUISignal(1, 1);
            }
            else
            {
                MessageBox.Show(uFCoder.status2str(status));
            }
        }

        private void bReaderClose_Click(object sender, EventArgs e)
        {
            status = uFCoder.ReaderClose();

            if (status == 0)
            {
                uFCoder.ReaderUISignal(1, 1);
            }
            else
            {
                MessageBox.Show(uFCoder.status2str(status));
            }
        }


        unsafe int TryEmvPseCardRead(string df_name, string szTitlePse)
        {
            EMV_STATUS emv_status;


            tCNR1.Clear();
            tCNR2.Clear();
            tCNR3.Clear();
            tCNR4.Clear();

            uFCoder reader = new uFCoder();

            bool head_attached = false;

            emv_tree_node_t head = new emv_tree_node_t();
            emv_tree_node_t tail = new emv_tree_node_t();
            emv_tree_node_t temp = new emv_tree_node_t();

            afl_list_item_t afl_list = new afl_list_item_t();
            afl_list_item_t afl_list_item = new afl_list_item_t();

            byte afl_list_count;
            byte[] r_apdu = new byte[258];
            int Ne;
            byte[] aid = new byte[16];
            char[] chr_aid = new char[16];
            byte[] sw = new byte[2];

            byte sfi, record = 0, cnt = 1, aid_len = 0;

            byte[] gpo_data_field = new byte[4];
            ushort gpo_data_field_size = 0;

            bool card_found = false;
            byte[] card_nr = new byte[8];
            int card_nr_len = 0;

            do
            {
                status = uFCoder.SetISO14443_4_Mode();

                if (status > 0)
                {
                    ReadPSERtb.AppendText("Error while switching into ISO 14443 - 4 mode, uFR status is: " + uFCoder.status2str(status));
                    break;
                }

                ReadPSERtb.AppendText(cnt++.ToString() + ". Issuing \"Select PSE\" command:  \n");
                ReadPSERtb.AppendText(" [C] 00 A4 04 00 " + BitConverter.ToString(Encoding.ASCII.GetBytes(df_name)).Replace("-", " ") + " 00");


                Ne = 256;

                status = uFCoder.APDUTransceive(0x00, 0xA4, 0x04, 0x00, df_name.ToCharArray(), df_name.Length, r_apdu, &Ne, 1, sw);

                if (status > 0)
                {
                    ReadPSERtb.AppendText("Error while executing APDU command, uFR status: " + uFCoder.status2str(status));
                    break;
                }
                else
                {
                    if (sw[0] != 0x90)
                    {
                        ReadPSERtb.AppendText("\n[SW]: " + sw[0].ToString("X2") + sw[1].ToString("X2") + "\n");
                        ReadPSERtb.AppendText("Could not continue execution due to an APDU error. \n");
                        break;
                    }

                    if (Ne > 0)
                    {
                        ReadPSERtb.AppendText("\n APDU command executed, response data length: " + Ne.ToString() + "\n");
                        ReadPSERtb.AppendText("[R] ");

                        for (int resp = 0; resp < Ne; resp++)
                        {
                            ReadPSERtb.AppendText(r_apdu[resp].ToString("X2") + ":");
                        }
                    }

                    ReadPSERtb.AppendText("\n[SW]: " + sw[0].ToString("X2") + sw[1].ToString("X2") + "\n");

                }


                emv_status = reader.newEmvTag(ref head, r_apdu, Ne, false);

                if (emv_status > 0)
                {
                    ReadPSERtb.AppendText("EMV parsing error code: " + emv_status.ToString());
                    break;
                }


                emv_status = reader.getSfi(ref head, &sfi);

                if (emv_status == 0)
                {
                    record = 1;

                    do
                    {
                        ReadPSERtb.AppendText("\n" + cnt.ToString() + ". Issuing \"Read Record\" command (record = " + record.ToString() + ", sfi = " + sfi.ToString() + ")\n");
                        ReadPSERtb.AppendText("[C] 00 B2 " + record.ToString("X2") + " " + ((sfi << 3) | 4).ToString("X2") + " 00\n");

                        emv_status = reader.emvReadRecord(r_apdu, &Ne, sfi, record, sw);
                        if (emv_status == 0)
                        {
                            emv_status = reader.newEmvTag(ref temp, r_apdu, Ne, false);

                            if (!head_attached)
                            {
                                head.next = temp;
                                head_attached = true;
                            }
                            else
                            {
                                tail.next = temp;
                                tail = tail.next;
                            }

                            if (Ne > 0)
                            {
                                ReadPSERtb.AppendText(" APDU command executed: response data length = " + Ne.ToString() + "\n");
                                ReadPSERtb.AppendText("[R] ");

                                for (int resp = 0; resp < Ne; resp++)
                                {
                                    ReadPSERtb.AppendText(r_apdu[resp].ToString("X2") + ":");
                                }
                            }
                            ReadPSERtb.AppendText("\n[SW] " + sw[0].ToString("X2") + sw[1].ToString("X2") + "\n");

                        }
                        else
                        {
                            if (sw[0] != 0x90)
                            {
                                ReadPSERtb.AppendText("\n[SW] " + sw[0].ToString("X2") + sw[1].ToString("X2") + "\n");
                                ReadPSERtb.AppendText("There is no records.\n");
                            }
                        }

                        record++;
                        cnt++;
                    } while (emv_status == 0);

                }

                emv_status = reader.getAid(ref head, ref aid, ref aid_len);
                if (emv_status == 0)

                    Array.Copy(aid, chr_aid, aid.Length);
                {
                    ReadPSERtb.AppendText("\n " + cnt++.ToString() + ". Issuing \"Select the appplication command\": \n");
                    ReadPSERtb.AppendText(" [C] 00 A4 00 00 " + aid_len.ToString("X2") + " ");
                    for (int print = 0; print < aid_len; print++)
                    {
                        ReadPSERtb.AppendText(aid[print].ToString("X2") + " ");
                    }
                    ReadPSERtb.AppendText(" 00");
                    Ne = 256;
                   

                    status = uFCoder.APDUTransceive(0x00, 0xA4, 0x04, 0x00, chr_aid, aid_len, r_apdu, &Ne, 1, sw);

                    if (status != 0)
                    {
                        ReadPSERtb.AppendText(" Error while executing APDU command, uFR status is: " + uFCoder.status2str(status) + "\n");

                    }
                    else
                    {
                        if (sw[0] != 0x90)
                        {
                            ReadPSERtb.AppendText("\n[SW] " + sw[0].ToString("X2") + sw[1].ToString("X2") + "\n");
                            ReadPSERtb.AppendText("Could not continue execution due to an APDU error.\n");
                            break;
                        }

                        if (Ne > 0)
                        {
                            ReadPSERtb.AppendText("\n APDU command executed: response data length = " + Ne.ToString() + " bytes \n");
                            ReadPSERtb.AppendText("[R] ");
                            for (int resp = 0; resp < Ne; resp++)
                            {
                                ReadPSERtb.AppendText(r_apdu[resp].ToString("X2") + ":");
                            }
                        }
                        ReadPSERtb.AppendText("\n[SW] " + sw[0].ToString("X2") + sw[1].ToString("X2") + "\n");
                    }

                    emv_status = reader.newEmvTag(ref temp, r_apdu, Ne, false);
                    if (emv_status > 0)
                    {
                        ReadPSERtb.AppendText(" EMV parsing error code: " + emv_status.ToString());
                        break;
                    }

                    if (!head_attached)
                    {
                        head.next = tail = temp;
                        head_attached = true;
                    }
                    else
                    {
                        tail.next = temp;
                        tail = tail.next;
                    }

                    ReadPSERtb.AppendText("\n " + cnt++.ToString() + ". Formatting \"Get Processing Options\" instruction (checking PDOL).\n");
                    emv_status = reader.formatGetProcessingOptionsDataField(temp, ref gpo_data_field, &gpo_data_field_size);

                    if (emv_status > 0)
                    {
                        ReadPSERtb.AppendText("EMV parsing error code: " + emv_status.ToString());
                        break;
                    }

                    ReadPSERtb.AppendText("\n " + cnt++.ToString() + ". Issuing \"Get Processing options\" command:\n");
                    ReadPSERtb.AppendText(" [C] 80 A8 00 00 " + gpo_data_field_size.ToString("X2") + " ");
                    for (int comm = 0; comm < gpo_data_field_size; comm++)
                    {
                        ReadPSERtb.AppendText(gpo_data_field[comm].ToString("X2") + " ");
                    }
                    ReadPSERtb.AppendText("00\n");

                    Ne = 256;

                    char[] chr_gpo_array = new char[gpo_data_field_size];

                    Array.Copy(gpo_data_field, chr_gpo_array, gpo_data_field.Length);


                    status = uFCoder.APDUTransceive_Bytes(0x80, 0xA8, 0x00, 0x00, gpo_data_field, gpo_data_field_size, r_apdu, &Ne, 1, sw);

                    if (status != 0)
                    {
                        ReadPSERtb.AppendText(" Error while executing APDU command, uFR status: " + uFCoder.status2str(status) + "\n");

                        break;
                    }
                    else
                    {

                        if (sw[0] != 0x90)
                        {
                            ReadPSERtb.AppendText("\n[SW] " + sw[0].ToString("X2") + sw[1].ToString("X2"));
                            ReadPSERtb.AppendText("Could not continue execution due to an APDU error.");
                            break;
                        }
                        if (Ne > 0)
                        {
                            ReadPSERtb.AppendText("APDU command executed: response data length = " + Ne.ToString() + "\n");
                            ReadPSERtb.AppendText("[R] ");
                            for (int resp = 0; resp < Ne; resp++)
                            {
                                ReadPSERtb.AppendText(r_apdu[resp].ToString("X2") + ":");
                            }
                        }

                        ReadPSERtb.AppendText("\n[SW] " + sw[0].ToString("X2") + sw[1].ToString("X2"));
                    }

                    emv_status = reader.newEmvTag(ref temp, r_apdu, Ne, false);
                    if (emv_status > 0)
                    {
                        ReadPSERtb.AppendText(" EMV parsing error code: " + emv_status.ToString());
                        break;
                    }
                    tail.next = temp;
                    tail = tail.next;

                    emv_status = reader.getAfl(temp, afl_list, &afl_list_count);

                    if (emv_status == EMV_STATUS.EMV_ERR_TAG_NOT_FOUND)
                    {
                         emv_status = reader.getAflFromResponseMessageTemplateFormat1(temp, afl_list, &afl_list_count);
                    }

                    if (emv_status > 0)
                    {
                        ReadPSERtb.AppendText(" EMV parsing error code: " + emv_status.ToString());
                        break;
                    }

                    afl_list_item = afl_list.next;

                    while (afl_list_item != null)
                    {
                        for (int r = afl_list_item.record_first; r <= afl_list_item.record_last; r++)
                        {
                            ReadPSERtb.AppendText("\n" + cnt.ToString() + ". Issuing \"Read Record\" command (record = " + r.ToString() + ", sfi = " + afl_list_item.sfi.ToString() + "):\n");

                            ReadPSERtb.AppendText(" [C] 00 B2 " + r.ToString("X2") + " " + ((afl_list_item.sfi << 3) | 4).ToString("X2") + " 00\n");
                           
                            emv_status = reader.emvReadRecord(r_apdu, &Ne, afl_list_item.sfi, (byte)r, sw);

                            if (card_found == false)
                            { 
                            card_found = reader.GetCardNumber(r_apdu, (int)Ne, ref card_nr, &card_nr_len);
                            }
                            if (emv_status == 0)
                            {
                                byte[] emv_apdu = new byte[r_apdu.Length];

                                Array.Copy(r_apdu, emv_apdu, r_apdu.Length);

                                emv_status = reader.newEmvTag(ref temp, emv_apdu, Ne, false);
                                if (emv_status == 0)
                                {
                                    tail.next = temp;
                                    tail = tail.next;
                                }
                                if (Ne > 0)
                                {
                                    ReadPSERtb.AppendText("APDU command executed: response data length = " + Ne.ToString() + "\n");
                                    ReadPSERtb.AppendText("[R] ");
                                    for (int resp = 0; resp < Ne; resp++)
                                    {
                                        ReadPSERtb.AppendText(r_apdu[resp].ToString("X2") + ":");
                                    }
                                }

                                ReadPSERtb.AppendText("\n[SW] " + sw[0].ToString("X2") + sw[1].ToString("X2"));
                            }
                            else
                            {
                                if (sw[0] != 0x90)
                                {
                                    ReadPSERtb.AppendText("\n[SW] " + sw[0].ToString("X2") + sw[1].ToString("X2"));
                                }
                            }
                            cnt++;
                        }
                        afl_list_item = afl_list_item.next;

                    }
                }                            

            } while (false);
            tCNR1.AppendText(card_nr[0].ToString("X2") + " " + card_nr[1].ToString("X2"));
            tCNR2.AppendText(card_nr[2].ToString("X2") + " " + card_nr[3].ToString("X2"));
            tCNR3.AppendText(card_nr[4].ToString("X2") + " " + card_nr[5].ToString("X2"));
            tCNR4.AppendText(card_nr[6].ToString("X2") + " " + card_nr[7].ToString("X2"));
                       
            uFCoder.s_block_deselect(100);
            
            return 0;



        }
        
        private void bReadTransactions_Click(object sender, EventArgs e)
        {
            unsafe
            {
                dLogs.Rows.Clear();
                tDebug.Clear();

                if (rPSE2.Checked)
                {
                    tryEmvPseLog("2PAY.SYS.DDF01", "PSE2");
                }
                else if (rPSE1.Checked)
                {
                    tryEmvPseLog("1PAY.SYS.DDF01", "PSE1");
                }
                else
                {
                    MessageBox.Show("You must select Payment System Environment (PSE1/PSE2) first.");
                }
            }
        }

        private void bClearTransactions_Click(object sender, EventArgs e)
        {
            uFCoder.s_block_deselect(100);
            tDebug.Clear();
            dLogs.Rows.Clear();
        }

        private void CheckPseButton_Click(object sender, EventArgs e)
        {
            unsafe
            {
                CheckPSERtb.Clear();


                if (rPSE2.Checked)
                {
                    checkEmvPse("2PAY.SYS.DDF01", "PSE2");
                }
                else if (rPSE1.Checked)
                {
                    checkEmvPse("1PAY.SYS.DDF01", "PSE1");
                }
                else
                {
                    MessageBox.Show("You must select Payment System Environment (PSE1/PSE2) first.");
                }

            }
        }

        public unsafe void checkEmvPse(string df_name, string szTitlePse)
        {

            emv_tree_node_t head = new emv_tree_node_t();
            emv_tree_node_t tail = new emv_tree_node_t();
            emv_tree_node_t temp = new emv_tree_node_t();
            uFCoder reader = new uFCoder();
            byte[] r_apdu = new byte[258];
            int Ne = 0;
            byte[] sw = new byte[2];
            byte sfi = 0, record = 0, cnt = 0;
            EMV_STATUS emv_status;
          
            do
            {
               status = uFCoder.SetISO14443_4_Mode();

                if (status != 0)
                {
                    CheckPSERtb.AppendText("Error while switching into ISO 14443-4 mode, uFR status is: " + uFCoder.status2str(status));
                    break;
                }
                CheckPSERtb.AppendText("1. Issuing \"Select PSE\" command (" + szTitlePse + "):\n");
                CheckPSERtb.AppendText(" [C] 00 A4 04 00 " + BitConverter.ToString(Encoding.ASCII.GetBytes(df_name)).Replace("-", " ") + " 00");
                Ne = 256;
                status = uFCoder.APDUTransceive(0x00, 0xA4, 0x04, 0x00, df_name.ToCharArray(), df_name.Length, r_apdu, &Ne, 1, sw);

                if (status != 0)
                {
                    CheckPSERtb.AppendText("Error while executing APDU command, uFR status is: " + uFCoder.status2str(status));
                    break;
                }
                else
                {
                    if (sw[0] != 0x90)
                    {
                        CheckPSERtb.AppendText("\n [SW] " + sw[0].ToString("X2") + " " + sw[1].ToString("X2"));

                        CheckPSERtb.AppendText("\nCould not continue execution due to an APDU error.\n");
                        break;
                    }
                    if (Ne > 0)
                    {
                        CheckPSERtb.AppendText("\n APDU command executed: response data length = " + Ne.ToString() + " bytes\n");
                        CheckPSERtb.AppendText(" [R] ");
                        for (int resp = 0; resp < Ne; resp++)
                        {
                            CheckPSERtb.AppendText(r_apdu[resp].ToString("X2") + ":");
                        }

                    }

                    CheckPSERtb.AppendText("\n [SW] " + sw[0].ToString("X2") + " " + sw[1].ToString("X2"));
                    
                }

                emv_status = reader.newEmvTag(ref head, r_apdu, Ne, false);

                if (emv_status != 0)
                {

                    CheckPSERtb.AppendText("EMV parsing error code: " + emv_status.ToString());
                    break;

                }

                emv_status = reader.getSfi(ref head, &sfi);

                if (emv_status == 0)
                {
                    cnt = 2;
                    record = 1;
                    do
                    {
                        CheckPSERtb.AppendText("\n" + cnt.ToString() + ". Issuing \"Read Record\" command (record = " + record.ToString() + ", sfi = " + sfi.ToString() + ")\n");
                        CheckPSERtb.AppendText("[C] 00 B2 " + record.ToString("X2") + " " + ((sfi << 3) | 4).ToString("X2") + " 00\n");

                        emv_status = reader.emvReadRecord(r_apdu, &Ne, sfi, record, sw);
                        if (emv_status == 0)
                        {
                            emv_status = reader.newEmvTag(ref temp, r_apdu, Ne, false);

                            if (record == 1)
                            {
                                head.next = tail = temp;

                            }
                            else
                            {
                                tail.next = temp;
                                tail = tail.next;
                            }

                            if (Ne > 0)
                            {
                                CheckPSERtb.AppendText(" APDU command executed: response data length = " + Ne.ToString() + "\n");
                                CheckPSERtb.AppendText("[R] ");

                                for (int resp = 0; resp < Ne; resp++)
                                {
                                    CheckPSERtb.AppendText(r_apdu[resp].ToString("X2") + ":");
                                }
                            }
                            CheckPSERtb.AppendText("\n [SW] " + sw[0].ToString("X2") + sw[1].ToString("X2") + "\n");

                        }
                        else
                        {
                            if (sw[0] != 0x90)
                            {
                                CheckPSERtb.AppendText("\n [SW] " + sw[0].ToString("X2") + sw[1].ToString("X2") + "\n");
                                CheckPSERtb.AppendText("There is no records.\n");
                            }
                        }

                        record++;
                        cnt++;
                    } while (emv_status == 0);
                }

                CheckPSERtb.AppendText("\n--------------------------------------------------------------------------------------------------------------------------------------\n");
                CheckPSERtb.AppendText("          Card supports Payment System Environment: " + szTitlePse);
                CheckPSERtb.AppendText("\n===================================================================\n");
                                        
            } while (false);

            uFCoder.s_block_deselect(100);

        }

        private void ReadPSEButton_Click(object sender, EventArgs e)
        {
            unsafe
            {
                ReadPSERtb.Clear();

                if (rPSE2.Checked)
                {
                    TryEmvPseCardRead("2PAY.SYS.DDF01", "PSE2");
                }
                else if (rPSE1.Checked)
                {
                    TryEmvPseCardRead("1PAY.SYS.DDF01", "PSE1");
                }
                else
                {
                    MessageBox.Show("You must select Payment System Environment (PSE1/PSE2) first.");
                }
            }
        }

        private void CheckPSEClear_Click(object sender, EventArgs e)
        {
            uFCoder.s_block_deselect(100);

            CheckPSERtb.Clear();
        }

        private void ReadParsePSEClear_Click(object sender, EventArgs e)
        {
            uFCoder.s_block_deselect(100);


            ReadPSERtb.Clear();
            tCNR1.Clear();
            tCNR2.Clear();
            tCNR3.Clear();
            tCNR4.Clear();
        }
    }
    
}


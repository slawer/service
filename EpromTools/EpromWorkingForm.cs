using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform;
using Microsoft.VisualBasic;

namespace EpromWorking
{
    public partial class EpromWorkingForm : Form
    {
        delegate void Maker();

        private Rectangle copedRectangle = null;
        private Rectangle canceledRectangle = null;

        private Eprom eprom = null;
        public IApplication app = null;
        private string oldValue = string.Empty;
        private WorkHandle workHandle = null;
        
        Device device = Device.D1;
        PageNumber page = PageNumber.P1;
        private AutoResetEvent mevent;
        ReadInfo rInfo;
        Incer incer;
        Initer initer;
        string data = string.Empty;

        public EpromWorkingForm()
        {
            InitializeComponent();

            share = new List<Packet>();
            working = new List<Packet>();
            mevent = new AutoResetEvent(false);
            workHandle = new WorkHandle();
            
            incer = new Incer(IncP);
            initer = new Initer(InitProgressBar);
        }

        // ------ буферизация --------

        private List<Packet> share;
        private List<Packet> working;

        public void Packet(Packet packet)
        {
            lock (workHandle)
            {
                if (workHandle.need == NeedTolk.Yes)
                {
                    if (app.GetProtocol(ProtocolVersion.x100).IsFromDevice(packet.packet))
                    {
                        if (workHandle.useBroadcast || app.GetProtocol(ProtocolVersion.x100).GetNumberDevice(packet.packet) ==
                            workHandle.deviceNumber)
                        {
                            lock (share)
                            {
                                share.Add(packet);
                            }
                            lock (mevent) mevent.Set();
                        }
                    }
                }
            }
        }
        private DataGridView[] Pages
        {
            get
            {
                DataGridView[] pages = new DataGridView[7];

                pages[0] = Page1;
                pages[1] = Page2;
                pages[2] = Page3;
                pages[3] = Page4;
                pages[4] = Page5;
                pages[5] = Page6;
                pages[6] = Page7;
                return pages;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DataGridView grid in Pages)
            {
                for (int i = 0; i < 16; i++)
                {
                    DataGridViewRow r = new DataGridViewRow();
                    r.Resizable = DataGridViewTriState.False;
                    grid.Rows.Add(r);
                }
            }

            comboBoxAlgorithmWrite.SelectedIndex = 0;
        }
        private void buttonSaveToFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                app.GetEFSaver(FileFormat.EF2XML).Save(saveFileDialog.FileName, GetTables());
            }
        }
        private void buttonLoadFromFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                IEFLoader ldr = null;
                switch (openFileDialog.FilterIndex)
                {
                    case 1:

                        ldr = app.GetEFLoader(FileFormat.EF1TXT);
                        break;

                    case 2:

                        ldr = app.GetEFLoader(FileFormat.EF2XMLOLD);
                        break;

                    case 3:

                        ldr = app.GetEFLoader(FileFormat.EF2XML);
                        break;
                }

                try
                {
                    eprom = ldr.Load(openFileDialog.FileName);
                    InitTabes(eprom);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Не удалось загрузить файл. Возможно файл не соответствует формату или поврежден",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
            }
        }

        // ----- загрузка EPROM с диска ----

        private void InitTabes(Eprom eprom)
        {
            int pageIndex = 0;
            foreach (Page page in eprom.Pages)
            {
                DataGridView p = GetPage(pageIndex++);
                if (p != null)
                {
                    for (int row = 0; row < 16; row++)
                    {
                        for (int col = 0; col < 16; col++)
                        {
                            int offset = col + row * 16;
                            p[col, row].Value = string.Format("{0:X2}", page[offset]);
                        }
                    }
                }
            }
        }
        public Eprom GetTables()
        {
            Eprom eprom = new Eprom();

            for (int i = 0; i < 7; i++)
            {
                DataGridView p = GetPage(i);
                if (p != null)
                {
                    for (int row = 0; row < 16; row++)
                    {
                        for (int col = 0; col < 16; col++)
                        {
                            if (p[col, row].Value != null && p[col, row].Value.ToString() != string.Empty)
                            {
                                eprom[i][col + row * 16] = (byte)int.Parse(p[col, row].Value.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
                            }
                        }
                    }
                }
            }
            return eprom;
        }
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if (box.Checked)
            {
                checkBoxPage1.Checked = true;
                checkBoxPage2.Checked = true;
                checkBoxPage3.Checked = true;
                checkBoxPage4.Checked = true;
                checkBoxPage5.Checked = true;
                checkBoxPage6.Checked = true;
                checkBoxPage7.Checked = true;
            }
            else
            {
                checkBoxPage1.Checked = false;
                checkBoxPage2.Checked = false;
                checkBoxPage3.Checked = false;
                checkBoxPage4.Checked = false;
                checkBoxPage5.Checked = false;
                checkBoxPage6.Checked = false;
                checkBoxPage7.Checked = false;
            }
        }
        private DataGridView GetActivedPage()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:

                    return Page1;

                case 1:

                    return Page2;

                case 2:

                    return Page3;

                case 3:

                    return Page4;

                case 4:

                    return Page5;

                case 5:

                    return Page6;

                case 6:

                    return Page7;
            }
            return null;
        }
        private DataGridView GetPage(int index)
        {
            switch (index)
            {
                case 0:

                    return Page1;

                case 1:

                    return Page2;

                case 2:

                    return Page3;

                case 3:

                    return Page4;

                case 4:

                    return Page5;

                case 5:

                    return Page6;

                case 6:

                    return Page7;
            }
            return null;
        }
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                copedRectangle = new Rectangle(GetActivedPage().SelectedCells);
            }
            catch (Exception) { }
        }
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetActivedPage().SelectedCells.Count == 1)
                {
                    int col = GetActivedPage().SelectedCells[0].ColumnIndex;
                    int row = GetActivedPage().SelectedCells[0].RowIndex;

                    for (int r = 0; r < copedRectangle.RowsCount; r++)
                    {
                        for (int c = 0; c < copedRectangle.ColumnsCount; c++)
                        {
                            if ((c + col) < GetActivedPage().ColumnCount && (r + row) < GetActivedPage().RowCount)
                            {
                                GetActivedPage()[c + col, r + row].Selected = true;
                            }
                        }
                    }
                }

                canceledRectangle = new Rectangle(GetActivedPage().SelectedCells);
                canceledRectangle.Paste(copedRectangle);
            }
            catch (Exception) { }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView page = GetActivedPage();

            page.CellBeginEdit -= Page_CellBeginEdit;
            page.CellValueChanged -= Page_CellValueChanged;

            canceledRectangle = new Rectangle(GetActivedPage().SelectedCells);
            if (page.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in page.SelectedCells)
                {
                    cell.Value = string.Empty;
                }
            }

            page.CellBeginEdit += Page_CellBeginEdit;
            page.CellValueChanged += Page_CellValueChanged;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (eprom != null)
            {
                InitTabes(eprom);
            }
        }
        private void Page_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int val = int.Parse((sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value.ToString(),
                    System.Globalization.NumberStyles.AllowHexSpecifier);

                if (val > 255)
                {
                    (sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value = oldValue;
                }

                (sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value = string.Format("{0:X2}", val);

            }
            catch (Exception)
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    (sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value = oldValue;
                }
            }
        }
        private void Page_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e != null)
            {
                if ((sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    oldValue = (sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value.ToString();
                }
            }
        }
        private void EpromWorkingForm_Shown(object sender, EventArgs e)
        {
            foreach (DataGridView grid in Pages)
            {
                grid.CellBeginEdit += Page_CellBeginEdit;
                grid.CellValueChanged += Page_CellValueChanged;
            }
        }
        private void Page1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                удалитьToolStripMenuItem_Click(sender, new EventArgs());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            lock (workHandle)
            {
                if (workHandle.operation == Operation.Default)
                {
                    workHandle.operation = Operation.SelectedRead;
                    workHandle.selected = new DataGridViewCell[GetActivedPage().SelectedCells.Count];

                    GetActivedPage().SelectedCells.CopyTo(workHandle.selected, 0);
                    Array.Sort(workHandle.selected, new Comparer());

                    workHandle.need = NeedTolk.Yes;
                    workHandle.commonTimeout = (int)numericUpDownDeviceAnswerTimeout.Value;
                    workHandle.defTimeout = (int)numericUpDownTimeoutForAnswer.Value;

                    if (checkBoxUseBroadcast.Checked)
                    {
                        workHandle.deviceNumber = 63;
                        device = Device.D3F;
                    }
                    else
                        workHandle.deviceNumber = (int)numericUpDownDeviceNumber.Value;

                    workHandle.countDo = (int)numericUpDownCountDo.Value;

                    Maker maker = new Maker(Do);
                    maker.BeginInvoke(null, null);
                }
            }
        }

        // ------- запись/чтение ------

        private void Do()
        {
            switch (workHandle.operation)
            {
                case Operation.Read:

                    ReadEprom();
                    break;

                case Operation.SelectedRead:

                    ReadSelect();
                    break;

                case Operation.SelectedWrite:

                    WriteSelect();
                    break;

                case Operation.Write:

                    WriteEprom();
                    break;

                default:

                    break;
            }            
        }

        // ----- инициализация прогресса --------

        private void InitProgressBar(int min, int max)
        {
            progressBar.Minimum = min;
            progressBar.Maximum = max;
            
            progressBar.Value = 0;
        }
        private void IncP(int value)
        {
            lock (workHandle)
            {
                if (workHandle.need == NeedTolk.Yes)
                {
                    progressBar.Increment(value);
                    Application.DoEvents();
                }
            }
        }

        private string CheckDatas(List<string> datas)
        {
            string result = string.Empty;
            if (rInfo.NumberOfDataChecks == 0) result = datas[0];

            if (datas.Count > 1)
            {
                List<string> bytes = new List<string>();
                for (int index = 0; index < 16; index++)
                {
                    bytes.Clear();
                    foreach (string data in datas)
                    {
                        bytes.Add(data.Substring(index * 2, 2));
                    }

                    bool fl = false;
                    for (int i = 0; i < bytes.Count; i++)
                    {
                        if (GetCountEqual(bytes[i], bytes, 2) == 2)
                        {
                            fl = true;
                            result += bytes[i];
                            break;
                        }
                    }
                    if (!fl) return string.Empty;                    
                }
            }
            return result;
        }
        private int GetCountEqual(string item, List<string> items, int maxCount)
        {
            int count = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (item == items[i])
                {
                    count += 1;
                    if (count == maxCount) return count;
                }
            }
            return count;
        }

        private void ReadEprom()
        {
            try
            {
                Invoke(initer, 0, rInfo.countCommands);
                                
                DataGridView page = null;
                List<string> Datas = new List<string>();

                IProtocol protocol = app.GetProtocol(ProtocolVersion.x100);                

                for (int pageIndex = 0; pageIndex < 7; pageIndex++)
                {
                    if (rInfo.Pages[pageIndex] != -1)
                    {
                        page = GetPage(pageIndex);
                        PageNumber pageNumber = PageNumber.P0;

                        if (rInfo.Pages[pageIndex] == 1) pageNumber = PageNumber.P1;
                        if (rInfo.Pages[pageIndex] == 2) pageNumber = PageNumber.P2;
                        if (rInfo.Pages[pageIndex] == 3) pageNumber = PageNumber.P3;
                        if (rInfo.Pages[pageIndex] == 4) pageNumber = PageNumber.P4;
                        if (rInfo.Pages[pageIndex] == 5) pageNumber = PageNumber.P5;
                        if (rInfo.Pages[pageIndex] == 6) pageNumber = PageNumber.P6;
                        if (rInfo.Pages[pageIndex] == 7) pageNumber = PageNumber.P7;                        

                        for (int lineIndex = 0; lineIndex < 16; lineIndex++)
                        {
                            int offset = lineIndex * 16;
                            string command = protocol.CreateCommand(rInfo.Device, Command.Read, pageNumber,
                                offset, 16, string.Empty);

                            bool needNext = true;
                            ResultOperation result = ResultOperation.Default;
                            for (int attempts = 0; attempts <= rInfo.NumberOfDataChecks; attempts++)
                            {
                                if (!needNext) break;
                                result = ChekerToReadEprom(new Packet(command, DateTime.Now, null));

                                switch (result)
                                {
                                    case ResultOperation.Succes:

                                        Datas.Add(protocol.GetData(data));
                                        data = CheckDatas(Datas);
                                        if (data != string.Empty)
                                        {
                                            Invoke(incer, 1);
                                            if (data.Length != 32)
                                            {
                                                MessageBox.Show("Поступившие данные не корректны.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            for (int i = 0; i < data.Length / 2; i++)
                                            {
                                                page[i, lineIndex].Value = data.Substring(i * 2, 2);
                                            }
                                            Datas.Clear();
                                            needNext = false;
                                        }
                                        break;

                                    case ResultOperation.Timeout:

                                        MessageBox.Show("Превышен лимит времени ожидания", "Сообщение", MessageBoxButtons.OK);
                                        return;

                                    case ResultOperation.MorePopit:

                                        MessageBox.Show("Превышен лимит попыток чтения/записи", "Сообщение", MessageBoxButtons.OK);
                                        return;

                                    default:

                                        break;
                                }
                                Thread.Sleep(rInfo.TimeoutBetweenRead);
                            }
                        }
                    }
                }
            }
            finally
            {
                workHandle.operation = Operation.Default;
            }
        }
        private void WriteEprom() 
        {
            try
            {
                Invoke(initer, 0, rInfo.countCommands);

                DataGridView page = null;
                List<string> Datas = new List<string>();

                IProtocol protocol = app.GetProtocol(ProtocolVersion.x100);

                if (rInfo.UsingAnAlgorithmWithDataProtection)
                {
                    string protectCommand = "@JOB#000#" + string.Format("{0:X2}", rInfo.DeviceNumber) + rInfo.ProtectionStart + "$";
                    app.SendPacket(new Packet(protectCommand, DateTime.Now, null));
                }

                for (int pageIndex = 0; pageIndex < 7; pageIndex++)
                {
                    if (rInfo.Pages[pageIndex] != -1)
                    {
                        page = GetPage(pageIndex);
                        PageNumber pageNumber = PageNumber.P0;

                        if (rInfo.Pages[pageIndex] == 1) pageNumber = PageNumber.P1;
                        if (rInfo.Pages[pageIndex] == 2) pageNumber = PageNumber.P2;
                        if (rInfo.Pages[pageIndex] == 3) pageNumber = PageNumber.P3;
                        if (rInfo.Pages[pageIndex] == 4) pageNumber = PageNumber.P4;
                        if (rInfo.Pages[pageIndex] == 5) pageNumber = PageNumber.P5;
                        if (rInfo.Pages[pageIndex] == 6) pageNumber = PageNumber.P6;
                        if (rInfo.Pages[pageIndex] == 7) pageNumber = PageNumber.P7;

                        for (int lineIndex = 0; lineIndex < 16; lineIndex++)
                        {
                            string wrdata = string.Empty;

                            for (int i = 0; i < 16; i++)
                            {
                                if (page[i, lineIndex].Value == null)
                                {
                                    MessageBox.Show("Конфигурация не записанна в устройство. Так как один из параметров имеет не допустимое значение",
                                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (page[i, lineIndex].Value.ToString() == string.Empty)
                                {
                                    MessageBox.Show("Конфигурация не записанна в устройство. Так как один из параметров имеет не допустимое значение", 
                                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                wrdata += page[i, lineIndex].Value.ToString();
                            }

                            int offset = lineIndex * 16;
                            string command = protocol.CreateCommand(rInfo.Device, Command.ReadWrite, pageNumber,
                                offset, 16, wrdata);

                            bool needNext = true;
                            ResultOperation result = ResultOperation.Default;
                            for (int attempts = 0; attempts <= rInfo.NumberOfDataChecks; attempts++)
                            {
                                if (!needNext) break;
                                result = ChekerToWriteEprom(new Packet(command, DateTime.Now, null));

                                switch (result)
                                {
                                    case ResultOperation.Succes:

                                        Datas.Add(protocol.GetData(data));
                                        data = CheckDatas(Datas);
                                        if (data != string.Empty)
                                        {
                                            Invoke(incer, 1);

                                            if (data.Length != 32)
                                            {
                                                MessageBox.Show("Поступившие данные не корректны. Скорее всего к линии подключенно более одного устройства.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            Datas.Clear();
                                            needNext = false;
                                        }
                                        break;

                                    case ResultOperation.Timeout:

                                        Datas.Clear();
                                        MessageBox.Show("Превышен лимит времени ожидания", "Сообщение", MessageBoxButtons.OK);
                                        return;

                                    case ResultOperation.MorePopit:

                                        Datas.Clear();
                                        MessageBox.Show("Превышен лимит попыток чтения/записи", "Сообщение", MessageBoxButtons.OK);
                                        return;

                                    default:

                                        Datas.Clear();
                                        break;
                                }
                                Thread.Sleep(rInfo.TimeoutBetweenRead);
                            }
                        }
                    }
                }
            }
            finally
            {
                if (rInfo.UsingAnAlgorithmWithDataProtection)
                {
                    string restartCommand = "@JOB#000#" + string.Format("{0:X2}", rInfo.DeviceNumber) + rInfo.ProtectionEnd + "$";
                    app.SendPacket(new Packet(restartCommand, DateTime.Now, null));
                }
                workHandle.operation = Operation.Default;
            }
        }

        private void ReadSelect()
        {
            try
            {
                Invoke(initer, 0, workHandle.selected.Length);
                foreach (DataGridViewCell cell in workHandle.selected)
                {
                    lock (workHandle)
                    {
                        if (workHandle.need == NeedTolk.No)
                        {
                            return;
                        }
                    }

                    int offset = (cell.RowIndex * 16) + cell.ColumnIndex;
                    string command = app.GetProtocol(ProtocolVersion.x100).CreateCommand(device, Command.Read, page,
                        offset, 0x01, string.Empty);

                    ResultOperation result = ChekerToRead(new Packet(command, DateTime.Now, cell));
                    switch (result)
                    {
                        case ResultOperation.Succes:
                                                        
                            break;

                        case ResultOperation.Timeout:

                            MessageBox.Show("Превышен лимит времени ожидания", "Сообщение", MessageBoxButtons.OK);
                            return;
                        case ResultOperation.MorePopit:

                            MessageBox.Show("Превышен лимит попыток чтения/записи", "Сообщение", MessageBoxButtons.OK);
                            return;

                        default:

                            break;
                    }
                    Thread.Sleep(workHandle.defTimeout);
                }
            }
            finally
            {
                workHandle.operation = Operation.Default;
            }
        }
        private void WriteSelect()
        {
            try
            {
                Incer incer = new Incer(IncP);
                Initer initer = new Initer(InitProgressBar);

                Invoke(initer, 0, workHandle.selected.Length);
                if (workHandle.needSafe)
                {
                    string cmd = "@JOB#000#" + string.Format("{0:X2}", workHandle.deviceNumber) + "09012002A00100$";
                    app.SendPacket(new Packet(cmd, DateTime.Now, null));
                }
                foreach (DataGridViewCell cell in workHandle.selected)
                {
                    lock (workHandle)
                    {
                        if (workHandle.need == NeedTolk.No)
                        {
                            return;
                        }
                    }

                    if (cell.Value == null) continue;

                    int offset = (cell.RowIndex * 16) + cell.ColumnIndex;
                    string command = app.GetProtocol(ProtocolVersion.x100).CreateCommand(device, Command.ReadWrite, page,
                        offset, 0x01, cell.Value.ToString());

                    ResultOperation result = ChekerToWrite(new Packet(command, DateTime.Now, cell));
                    switch (result)
                    {
                        case ResultOperation.Succes:
                                                        
                            break;

                        case ResultOperation.Timeout:

                            MessageBox.Show("Превышен лимит времени ожидания ответа", "Сообщение", MessageBoxButtons.OK);
                            return;
                        case ResultOperation.MorePopit:

                            MessageBox.Show("Превышен лимит попыток чтения/записи", "Сообщение", MessageBoxButtons.OK);
                            return;

                        default:

                            break;
                    }
                    Thread.Sleep(workHandle.defTimeout);
                }
            }
            finally
            {
                if (workHandle.needSafe)
                {
                    string cmd = "@JOB#000#" + string.Format("{0:X2}", workHandle.deviceNumber) + "0705100100$";
                    app.SendPacket(new Packet(cmd, DateTime.Now, null));
                }
                workHandle.operation = Operation.Default;
            }
        }
        
        // ------- чекеры -------

        private ResultOperation ChekerToReadEprom(Packet packet)
        {
            try
            {
                for (int count = 0; count < workHandle.countDo; count++)
                {
                    app.SendPacket(packet);
                    long baseTime = DateTime.Now.Ticks;
                m:
                    if (mevent.WaitOne(workHandle.commonTimeout))
                    {
                        lock (share)
                        {
                            if (share.Count > 0)
                            {
                                working.AddRange(share);
                                share.Clear();
                            }
                        }

                        if (CheckOneToReadEprom(packet))
                        {
                            return ResultOperation.Succes;
                        }
                        else
                        {
                            long time = (long)((DateTime.Now.Ticks - baseTime) * 1E-4);
                            if (time > workHandle.commonTimeout)
                            {
                                return ResultOperation.Timeout;
                            }
                            else
                                goto m;
                        }
                    }
                }
                return ResultOperation.MorePopit;
            }
            finally
            {
                working.Clear();
            }
        }
        private ResultOperation ChekerToWriteEprom(Packet packet)
        {
            try
            {
                for (int count = 0; count < workHandle.countDo; count++)
                {
                    app.SendPacket(packet);
                    long baseTime = DateTime.Now.Ticks;
                m:
                    if (mevent.WaitOne(workHandle.commonTimeout))
                    {
                        lock (share)
                        {
                            if (share.Count > 0)
                            {
                                working.AddRange(share);
                                share.Clear();
                            }
                        }

                        if (CheckOneToWriteEprom(packet))
                        {
                            return ResultOperation.Succes;
                        }
                        else
                        {
                            long time = (long)((DateTime.Now.Ticks - baseTime) * 1E-4);
                            if (time > workHandle.commonTimeout)
                            {
                                return ResultOperation.Timeout;
                            }
                            else
                                goto m;
                        }
                    }
                }
                return ResultOperation.MorePopit;
            }
            finally
            {
                working.Clear();
            }
        }

        private ResultOperation ChekerToRead(Packet packet)
        {
            try
            {
                for (int count = 0; count < workHandle.countDo; count++)
                {
                    app.SendPacket(packet);
                    long baseTime = DateTime.Now.Ticks;
                m:
                    if (mevent.WaitOne(workHandle.commonTimeout))
                    {
                        lock (share)
                        {
                            if (share.Count > 0)
                            {
                                working.AddRange(share);
                                share.Clear();
                            }
                        }

                        if (CheckOneToRead(packet))
                        {
                            return ResultOperation.Succes;
                        }
                        else
                        {
                            long time = (long)((DateTime.Now.Ticks - baseTime) * 1E-4);
                            if (time > workHandle.commonTimeout) return ResultOperation.Timeout;
                            else
                                goto m;
                        }
                    }                    
                }
                return ResultOperation.MorePopit;
            }
            finally
            {
                working.Clear();
            }
        }
        private ResultOperation ChekerToWrite(Packet packet)
        {
            try
            {
                for (int count = 0; count < workHandle.countDo; count++)
                {
                    app.SendPacket(packet);
                    long baseTime = DateTime.Now.Ticks;
                m:
                    if (mevent.WaitOne(workHandle.commonTimeout))
                    {
                        lock (share)
                        {
                            if (share.Count > 0)
                            {
                                working.AddRange(share);
                                share.Clear();
                            }
                        }

                        if (CheckOneToWrite(packet))
                        {
                            return ResultOperation.Succes;
                        }
                        else
                        {
                            long time = (long)((DateTime.Now.Ticks - baseTime) * 1E-4);
                            if (time > workHandle.commonTimeout) return ResultOperation.Timeout;
                            else
                                goto m;
                        }
                    }
                }
                return ResultOperation.MorePopit;
            }
            finally
            {
                working.Clear();
            }
        }
        
        private bool CheckOneToReadEprom(Packet packet)
        {
            bool result = false;
            foreach (Packet pack in working)
            {
                if (isAnswerOnReadEprom(packet.packet, pack.packet) || rInfo.UseBroadcast)
                {
                    data = pack.packet;
                    return true;
                }
            }
            return result;
        }
        private bool CheckOneToWriteEprom(Packet packet)
        {
            bool result = false;
            foreach (Packet pack in working)
            {
                if (isAnswerOnWriteEprom(packet.packet, pack.packet) || rInfo.UseBroadcast)
                {
                    data = pack.packet;
                    return true;
                }
            }
            return result;
        }

        private bool CheckOneToRead(Packet packet)
        {
            bool result = false;
            foreach (Packet pack in working)
            {
                if (isAnswerOnRead(packet.packet, pack.packet) || workHandle.useBroadcast)
                {
                    Invoke(incer, 1);
                    (packet.Token as DataGridViewCell).Value = app.GetProtocol(ProtocolVersion.x100).GetData(pack.packet);
                    return true;
                }
            }
            return result;
        }
        private bool CheckOneToWrite(Packet packet)
        {
            bool result = false;
            foreach (Packet pack in working)
            {
                if (isAnswerOnWrite(packet.packet, pack.packet) || workHandle.useBroadcast)
                {
                    Invoke(incer, 1);
                    (packet.Token as DataGridViewCell).Value = app.GetProtocol(ProtocolVersion.x100).GetData(pack.packet);
                    return true;
                }
            }
            return result;
        }

        private bool isAnswerOnReadEprom(string question, string answer)
        {
            string fp = question.Substring(10, question.Length - 13);
            string sp = answer.Substring(3, 9/*answer.Length - 38*/);
            return (IntLPakLine(fp) == sp);
        }
        private bool isAnswerOnWriteEprom(string question, string answer)
        {
            string fp = question.Substring(10, question.Length - 45);
            string sp = answer.Substring(3, 9);
            return (fp == sp);
        }

        private bool isAnswerOnRead(string question, string answer)
        {
            string fp = question.Substring(10, question.Length - 13);
            string sp = answer.Substring(3, 9/*answer.Length - 8*/);
            return (IntLPak(fp) == sp);
        }
        private bool isAnswerOnWrite(string question, string answer)
        {
            string fp = question.Substring(10, question.Length - 13);
            if (answer.Length > fp.Length)
            {
                string sp = answer.Substring(3, 11/*answer.Length - 6*/);
                return (fp == sp);
            }
            return
                false;
        }

        private string IntLPakLine(string pac)
        {
            string total = pac.Substring(0, 1);
            string lp = pac.Substring(1, 2);
            lp = string.Format("{0:X2}", (int.Parse(lp, System.Globalization.NumberStyles.AllowHexSpecifier) + 16));
            total += lp + pac.Substring(3, pac.Length - 3);
            return total;
        }
        private string IntLPak(string pac)
        {
            string total = pac.Substring(0, 1);
            string lp = pac.Substring(1, 2);
            lp = string.Format("{0:X2}", (int.Parse(lp, System.Globalization.NumberStyles.AllowHexSpecifier) + 1));
            total += lp + pac.Substring(3, pac.Length - 3);
            return total;
        }
        
        // --------------------

        private void numericUpDownDeviceNumber_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            switch ((int)numeric.Value)
            {
                case 1: device = Device.D1; break;
                case 2: device = Device.D2; break;
                case 3: device = Device.D3; break;
                case 4: device = Device.D4; break;
                case 5: device = Device.D5; break;
                case 6: device = Device.D6; break;
                case 7: device = Device.D7; break;
                case 8: device = Device.D8; break;
                case 9: device = Device.D9; break;
                case 10: device = Device.D10; break;
                case 11: device = Device.D11; break;
                case 12: device = Device.D12; break;
                case 13: device = Device.D13; break;
                case 14: device = Device.D14; break;
                case 15: device = Device.D15; break;
                case 16: device = Device.D16; break;
                case 17: device = Device.D17; break;
                case 18: device = Device.D18; break;
                case 19: device = Device.D19; break;
                case 20: device = Device.D20; break;
                case 21: device = Device.D21; break;
                case 22: device = Device.D22; break;
                case 23: device = Device.D23; break;
                case 24: device = Device.D24; break;
                case 25: device = Device.D25; break;
                case 26: device = Device.D26; break;
                case 27: device = Device.D27; break;
                case 28: device = Device.D28; break;
                case 29: device = Device.D29; break;
                case 30: device = Device.D30; break;
                case 31: device = Device.D31; break;
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = sender as TabControl;
            switch (tab.SelectedIndex)
            {
                case 0:

                    page = PageNumber.P1;
                    break;

                case 1:

                    page = PageNumber.P2;
                    break;

                case 2:

                    page = PageNumber.P3;
                    break;

                case 3:

                    page = PageNumber.P4;
                    break;

                case 4:

                    page = PageNumber.P5;
                    break;

                case 5:

                    page = PageNumber.P6;
                    break;

                case 6:

                    page = PageNumber.P7;
                    break;

                default:

                    break;
            }
        }
        private void EpromWorkingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            lock (workHandle)
            {
                workHandle.need = NeedTolk.No;
            }

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            lock (workHandle)
            {
                if (workHandle.operation == Operation.Default)
                {
                    if (box.Checked)
                    {
                        workHandle.useBroadcast = true;
                        workHandle.deviceNumber = 63;
                        device = Device.D3F;
                    }
                    else
                    {
                        numericUpDownDeviceNumber_ValueChanged(numericUpDownDeviceNumber, new EventArgs());
                        workHandle.useBroadcast = false;
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            lock (workHandle)
            {
                if (workHandle.operation == Operation.Default)
                {
                    workHandle.operation = Operation.SelectedWrite;
                    workHandle.selected = new DataGridViewCell[GetActivedPage().SelectedCells.Count];

                    GetActivedPage().SelectedCells.CopyTo(workHandle.selected, 0);
                    Array.Sort(workHandle.selected, new Comparer());

                    if (comboBoxAlgorithmWrite.SelectedIndex == 1)
                    {
                        workHandle.needSafe = true;                        
                    }
                    
                    workHandle.need = NeedTolk.Yes;
                    workHandle.commonTimeout = (int)numericUpDownDeviceAnswerTimeout.Value;
                    workHandle.defTimeout = (int)numericUpDownTimeoutForAnswer.Value;

                    if (checkBoxUseBroadcast.Checked)
                    {
                        workHandle.deviceNumber = 63;
                        device = Device.D3F;
                    }
                    else
                        workHandle.deviceNumber = (int)numericUpDownDeviceNumber.Value;
                    
                    workHandle.countDo = (int)numericUpDownCountDo.Value;

                    Maker maker = new Maker(Do);
                    maker.BeginInvoke(null, null);
                }
            }
        }
        private void buttonReadFromDevice_Click(object sender, EventArgs e)
        {
            lock (workHandle)
            {
                if (workHandle.operation == Operation.Default)
                {
                    workHandle.operation = Operation.Read;
                    rInfo = new ReadInfo();

                    rInfo.Device = device;
                    rInfo.DeviceNumber = (int)numericUpDownDeviceNumber.Value;
                    rInfo.TimeoutForAnswer = (int)numericUpDownDeviceAnswerTimeout.Value;
                    rInfo.TimeoutBetweenRead = (int)numericUpDownTimeoutForAnswer.Value;
                    rInfo.AttemptsToRead = (int)numericUpDownCountDo.Value;
                    rInfo.NumberOfDataChecks = (int)numericUpDown5.Value;
                    rInfo.UseBroadcast = checkBoxUseBroadcast.Checked;

                    if (rInfo.UseBroadcast)
                    {
                        rInfo.Device = Device.D3F;
                        rInfo.DeviceNumber = 63;
                    }

                    if (comboBoxAlgorithmWrite.SelectedIndex == 0)
                    {
                        rInfo.UsingAnAlgorithmWithDataProtection = false;
                    }
                    else
                        rInfo.UsingAnAlgorithmWithDataProtection = true;

                    if (checkBoxAllPages.Checked)
                    {
                        for (int i = 0; i < rInfo.Pages.Length; i++)
                        {
                            rInfo.Pages[i] = (i + 1);
                        }
                        rInfo.countCommands = 112;
                    }
                    else
                    {
                        if (checkBoxPage1.Checked) { rInfo.Pages[0] = 1; rInfo.countCommands += 16; }
                        if (checkBoxPage2.Checked) { rInfo.Pages[1] = 2; rInfo.countCommands += 16; }
                        if (checkBoxPage3.Checked) { rInfo.Pages[2] = 3; rInfo.countCommands += 16; }
                        if (checkBoxPage4.Checked) { rInfo.Pages[3] = 4; rInfo.countCommands += 16; }
                        if (checkBoxPage5.Checked) { rInfo.Pages[4] = 5; rInfo.countCommands += 16; }
                        if (checkBoxPage6.Checked) { rInfo.Pages[5] = 6; rInfo.countCommands += 16; }
                        if (checkBoxPage7.Checked) { rInfo.Pages[6] = 7; rInfo.countCommands += 16; }
                    }

                    try
                    {
                        workHandle.need = NeedTolk.Yes;
                        workHandle.commonTimeout = rInfo.TimeoutForAnswer;
                        workHandle.defTimeout = rInfo.TimeoutBetweenRead;
                        workHandle.deviceNumber = rInfo.DeviceNumber;
                        workHandle.countDo = rInfo.AttemptsToRead;

                        Maker maker = new Maker(Do);
                        maker.BeginInvoke(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось прочитать конфигурацию устройства!" + Constants.vbCrLf +
                            "Причина: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonWriteToDevice_Click(object sender, EventArgs e)
        {
            lock (workHandle)
            {
                if (workHandle.operation == Operation.Default)
                {
                    workHandle.operation = Operation.Write;
                    rInfo = new ReadInfo();

                    rInfo.Device = device;
                    rInfo.DeviceNumber = (int)numericUpDownDeviceNumber.Value;
                    rInfo.TimeoutForAnswer = (int)numericUpDownDeviceAnswerTimeout.Value;
                    rInfo.TimeoutBetweenRead = (int)numericUpDownTimeoutForAnswer.Value;
                    rInfo.AttemptsToRead = (int)numericUpDownCountDo.Value;
                    rInfo.NumberOfDataChecks = (int)numericUpDown5.Value;
                    rInfo.UseBroadcast = checkBoxUseBroadcast.Checked;

                    if (rInfo.UseBroadcast)
                    {
                        rInfo.Device = Device.D3F;
                        rInfo.DeviceNumber = 63;
                    }

                    if (comboBoxAlgorithmWrite.SelectedIndex == 0)
                    {
                        rInfo.UsingAnAlgorithmWithDataProtection = false;
                    }
                    else
                        rInfo.UsingAnAlgorithmWithDataProtection = true;

                    if (checkBoxAllPages.Checked)
                    {
                        for (int i = 0; i < rInfo.Pages.Length; i++)
                        {
                            rInfo.Pages[i] = (i + 1);
                        }
                        rInfo.countCommands = 112;
                    }
                    else
                    {
                        if (checkBoxPage1.Checked) { rInfo.Pages[0] = 1; rInfo.countCommands += 16; }
                        if (checkBoxPage2.Checked) { rInfo.Pages[1] = 2; rInfo.countCommands += 16; }
                        if (checkBoxPage3.Checked) { rInfo.Pages[2] = 3; rInfo.countCommands += 16; }
                        if (checkBoxPage4.Checked) { rInfo.Pages[3] = 4; rInfo.countCommands += 16; }
                        if (checkBoxPage5.Checked) { rInfo.Pages[4] = 5; rInfo.countCommands += 16; }
                        if (checkBoxPage6.Checked) { rInfo.Pages[5] = 6; rInfo.countCommands += 16; }
                        if (checkBoxPage7.Checked) { rInfo.Pages[6] = 7; rInfo.countCommands += 16; }
                    }

                    try
                    {
                        workHandle.need = NeedTolk.Yes;
                        workHandle.commonTimeout = rInfo.TimeoutForAnswer;
                        workHandle.defTimeout = rInfo.TimeoutBetweenRead;
                        workHandle.deviceNumber = rInfo.DeviceNumber;
                        workHandle.countDo = rInfo.AttemptsToRead;

                        Maker maker = new Maker(Do);
                        maker.BeginInvoke(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось прочитать конфигурацию устройства!" + Constants.vbCrLf +
                            "Причина: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }
        private void остановитьВыполнениеОперацииЧтениязаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (workHandle)
            {
                workHandle.need = NeedTolk.No;
                workHandle.operation = Operation.Default;
            }
        }
        private void очиститьВсеСтраницыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                DataGridView page = GetPage(i);

                page.CellBeginEdit -= Page_CellBeginEdit;
                page.CellValueChanged -= Page_CellValueChanged;

                foreach (DataGridViewRow row in page.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Value = string.Empty;
                    }
                }

                page.CellBeginEdit += Page_CellBeginEdit;
                page.CellValueChanged += Page_CellValueChanged;
            }
        }
        private void страница1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView page = GetPage(0);

            page.CellBeginEdit -= Page_CellBeginEdit;
            page.CellValueChanged -= Page_CellValueChanged;

            foreach (DataGridViewRow row in page.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = string.Empty;
                }
            }

            page.CellBeginEdit += Page_CellBeginEdit;
            page.CellValueChanged += Page_CellValueChanged;
        }
        private void страница2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView page = GetPage(1);

            page.CellBeginEdit -= Page_CellBeginEdit;
            page.CellValueChanged -= Page_CellValueChanged;

            foreach (DataGridViewRow row in page.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = string.Empty;
                }
            }

            page.CellBeginEdit += Page_CellBeginEdit;
            page.CellValueChanged += Page_CellValueChanged;
        }
        private void страница3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView page = GetPage(2);

            page.CellBeginEdit -= Page_CellBeginEdit;
            page.CellValueChanged -= Page_CellValueChanged;

            foreach (DataGridViewRow row in page.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = string.Empty;
                }
            }

            page.CellBeginEdit += Page_CellBeginEdit;
            page.CellValueChanged += Page_CellValueChanged;
        }
        private void страница4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView page = GetPage(3);

            page.CellBeginEdit -= Page_CellBeginEdit;
            page.CellValueChanged -= Page_CellValueChanged;

            foreach (DataGridViewRow row in page.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = string.Empty;
                }
            }

            page.CellBeginEdit += Page_CellBeginEdit;
            page.CellValueChanged += Page_CellValueChanged;
        }
        private void страница5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView page = GetPage(4);

            page.CellBeginEdit -= Page_CellBeginEdit;
            page.CellValueChanged -= Page_CellValueChanged;

            foreach (DataGridViewRow row in page.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = string.Empty;
                }
            }

            page.CellBeginEdit += Page_CellBeginEdit;
            page.CellValueChanged += Page_CellValueChanged;
        }
        private void страница6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView page = GetPage(5);

            page.CellBeginEdit -= Page_CellBeginEdit;
            page.CellValueChanged -= Page_CellValueChanged;

            foreach (DataGridViewRow row in page.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = string.Empty;
                }
            }

            page.CellBeginEdit += Page_CellBeginEdit;
            page.CellValueChanged += Page_CellValueChanged;
        }
        private void страница7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView page = GetPage(6);

            page.CellBeginEdit -= Page_CellBeginEdit;
            page.CellValueChanged -= Page_CellValueChanged;

            foreach (DataGridViewRow row in page.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = string.Empty;
                }
            }

            page.CellBeginEdit += Page_CellBeginEdit;
            page.CellValueChanged += Page_CellValueChanged;
        }
    }

    class WorkHandle
    {
        public Operation operation = Operation.Default;
        public DataGridViewCell[] selected = null;
        public NeedTolk need = NeedTolk.No;
        public int commonTimeout = 0;
        public int defTimeout = 0;
        public int deviceNumber = 1;
        public int countDo = 0;
        public bool needSafe = false;
        public bool useBroadcast = false;
    }

    enum NeedTolk { Yes, No }
    enum ResultOperation { Succes, Timeout, MorePopit, Default }

    delegate void Initer(int min, int max);
    delegate void Incer(int value);
}
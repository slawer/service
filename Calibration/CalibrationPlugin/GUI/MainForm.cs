using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.VisualBasic;
using System.Globalization;

using Platform;
using Calibration.CalibrationPlugin.IO;

namespace Calibration.CalibrationPlugin.GUI
{
    public partial class MainForm : Form
    {
        private const int defaultTimeWaitOnPacketMutex = 3000;

        bool needCorrect = false;
        bool correctP = true;

        object oldValue = null;
        object newValue = null;

        // данные класса 

        private BIOS bios = null;                           // выполняет все операции ввода/вывода в данном плагине
        private IProtocol proto = null;

        private ObjectCurrentState currentState = null;     // хранит текущее состояние

        private Sync syncker = null;
        private GraphicCalibration gr = null;

        private bool biosTranslated = false;
        private Mutex packetSyncMutex = null;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="app">Ссылка на платформу</param>
        /// <param name="pBios">Ссылка на подсистему ввода/вывода платформы</param>
        public MainForm(IApplication app, IEpromIO pBios, IProtocol protocol)
        {
            InitializeComponent();
            textInserter = new TextInsert(InsertToText);

            oldValue = new object();
            newValue = new object();

            oldValue = "0";
            newValue = "0";

            bios = new BIOS(app, pBios);
            proto = protocol;

            currentState = new ObjectCurrentState();

            for (int i = 0; i < 11; i++)
            {
                DataGridViewRow r = new DataGridViewRow();
                if ((i % 2) == 0) r.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dataGridViewCalibrationTable.Rows.Add(r);
            }

            syncker = new Sync();
            packetSyncMutex = new Mutex(false);

            gr = new GraphicCalibration(CreateGraphics(), new Rectangle(12, 38, 422, 267));
            gr.CalculateScale();
        }

        TextInsert textInserter = null;
        private void InsertToText(TextBox box, string value)
        {
            box.Text = value;
        }

        /// <summary>
        /// номер устройства
        /// </summary>
        public int Device
        {
            get { return bios.BIOS_Options.Device; }
        }

        /// <summary>
        /// Пакет на обработку
        /// </summary>
        /// <param name="packet">Поступивший пакет</param>
        public void Packet(string packet)
        {
            bool blocked = false;
            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;
                    if (biosTranslated)
                    {
                        bios.Packet(packet);
                    }
                    else
                    {
                        //throw new Exception();
                        if (proto.PageAdress(packet) != 0) return;

                        string data = proto.GetData(packet);
                        int offsetInPacket = currentState.SelectedCalibrationTable.OffsetInPacket * 2;

                        if (data.Length > offsetInPacket + 4)
                        {
                            ushort calibrated = ushort.Parse(data.Substring(currentState.SelectedCalibrationTable.OffsetInPacket * 2, 4),
                                NumberStyles.AllowHexSpecifier);

                            ushort physic = (ushort)currentState.SelectedCalibrationTable.CalibrationTable.CalculateFromInPacket(calibrated);

                            gr.InsertPoint(new Point(physic, calibrated));
                            onShown(null, null);

                            Invoke(textInserter, textBoxFromDevicePhysic, physic.ToString());
                            Invoke(textInserter, textBoxFromDeviceCalibrated, calibrated.ToString());
                        }
                    }
                    blocked = false;
                    packetSyncMutex.ReleaseMutex();
                }
            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();
            }
        }

        // ------- события -------

        public event EventHandler TurnOnPackets;
        public event EventHandler TurnOffPackets;

        /// <summary>
        /// общие настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commonOptions_Click(object sender, EventArgs e)
        {
            OptionsForm opt = new OptionsForm();

            opt.numericUpDownDevice.Value = bios.BIOS_Options.Device;
            opt.numericUpDownDeviceAnswerTimeout.Value = bios.BIOS_Options.TimeoutForResponseFromDevice;
            opt.numericUpDownTimeoutForAnswer.Value = bios.BIOS_Options.TimeoutBetweenAttemptsToReadWrite;
            opt.numericUpDownCountDo.Value = bios.BIOS_Options.AttemptsToReadWriteEntries;
            opt.numericUpDownDataCheck.Value = bios.BIOS_Options.NumberOfDataChecks;

            if (opt.ShowDialog(this) == DialogResult.OK)
            {
                bios.BIOS_Options.Device = (int)opt.numericUpDownDevice.Value;
                bios.BIOS_Options.TimeoutForResponseFromDevice = (int)opt.numericUpDownDeviceAnswerTimeout.Value;
                bios.BIOS_Options.TimeoutBetweenAttemptsToReadWrite = (int)opt.numericUpDownTimeoutForAnswer.Value;
                bios.BIOS_Options.AttemptsToReadWriteEntries = (int)opt.numericUpDownCountDo.Value;
                bios.BIOS_Options.NumberOfDataChecks = (int)opt.numericUpDownDataCheck.Value;
            }
        }

        /// <summary>
        /// отрисовка графика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onShown(object sender, EventArgs e)
        {
            // нужна синхронизация
            if (!syncker.Blocked)
            {
                syncker.Block();
                gr.PresentAll();
                syncker.Relese();
            }
        }

        /// <summary>
        /// перерисовка нрафика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onPaint(object sender, PaintEventArgs e)
        {
            onShown(this, new EventArgs());
        }

        /// <summary>
        /// Загрузка данных с устройства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadFromDevice_Click(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;
                    biosTranslated = true;

                    blocked = false;
                    packetSyncMutex.ReleaseMutex();
                }
                else
                {
                    return;
                }

                if (!currentState.PacketsTurnOn)
                {
                    currentState.PacketsTurnOn = true;
                    TurnOnPackets(null, null);
                }
                LoadeForm loader = new LoadeForm(bios, currentState);

                if (loader.ShowDialog(this) == DialogResult.OK)
                {
                    if (currentState.Eprom == null)
                    {
                        MessageBox.Show(this, "Произошла не предвиденная ошибка!" +
                            Constants.vbCrLf + "не удалось загрузить таблицу описателей калибруемых параметров",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        if (currentState.CalibrationTableHandles == null)
                        {
                            MessageBox.Show(this, "Данные в EPROM устройстве не корректны!" +
                                Constants.vbCrLf + "Не удалось выделить таблицу описателей калибровочных параметров",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            currentState.Eprom = null;
                        }
                }

                loader.Close();
                loader.Dispose();

                gr.ResetPoint();
                gr.ResetPoints();

                gr.ResetScale();
                ClearDataGrid();

                onShown(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();

                currentState.PacketsTurnOn = false;
                TurnOffPackets(null, null);
            }
        }

        /// <summary>
        /// очистить таблицу на форме
        /// </summary>
        private void ClearDataGrid()
        {
            dataGridViewCalibrationTable.Rows.Clear();
            for (int i = 0; i < 11; i++)
            {
                DataGridViewRow r = new DataGridViewRow();

                if ((i % 2) == 0) r.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dataGridViewCalibrationTable.Rows.Add(r);
            }
        }

        /// <summary>
        /// показать форму выбора калибруемого канала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listOfChannels_Click(object sender, EventArgs e)
        {
            if (currentState.CalibrationTableHandles == null)
            {
                MessageBox.Show(this, "Не загруженна таблица калибровочных параметров",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ChannelsForm cha = new ChannelsForm();

            int i = 1;
            foreach (CalibrationTableHandle handle in currentState.CalibrationTableHandles)
            {
                ListViewItem item = new ListViewItem(i.ToString());

                ListViewItem.ListViewSubItem id = new ListViewItem.ListViewSubItem(item, string.Format("{0:X2}", handle.Name));
                ListViewItem.ListViewSubItem offset = new ListViewItem.ListViewSubItem(item, string.Format("{0:X2}", handle.Offset));
                ListViewItem.ListViewSubItem name = new ListViewItem.ListViewSubItem(item, "");

                item.Tag = handle;

                item.SubItems.Add(id);
                item.SubItems.Add(offset);
                item.SubItems.Add(name);

                cha.listViewChannels.Items.Add(item);
                i = i + 1;
            }

            if (cha.ShowDialog(this) == DialogResult.OK)
            {
                if (cha.listViewChannels.SelectedItems != null)
                {
                    if (cha.listViewChannels.SelectedItems[0].Tag is CalibrationTableHandle)
                    {
                        currentState.SelectedCalibrationTable = (CalibrationTableHandle)cha.listViewChannels.SelectedItems[0].Tag;
                        LastError lastError = bios.GetCalibrationTable(currentState.Eprom, currentState.SelectedCalibrationTable);

                        switch (lastError)
                        {
                            case LastError.Success:

                                ShowCalibrationTableInGraphics(currentState.SelectedCalibrationTable.CalibrationTable);
                                ShowCalibrationTableInDataGrid(currentState.SelectedCalibrationTable.CalibrationTable);

                                if (currentState.SelectedCalibrationTable.CalibrationTable.IsExtremCalculated)
                                {
                                    if (currentState.SelectedCalibrationTable.CalibrationTable.IsValidExtremPoints == false)
                                    {
                                        checkBoxCaculateSide.Checked = false;
                                        if (MessageBox.Show(this, "Крайние точки расчитаны не корректно" + Constants.vbCrLf +
                                                "Сбросить крайние точки в значение по умолчанию?", "Ошибка", MessageBoxButtons.YesNo, 
                                                MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                        {
                                            currentState.SelectedCalibrationTable.CalibrationTable.ClearExtremPoints();
                                        }
                                    }
                                    else
                                        checkBoxCaculateSide.Checked = true;
                                }
                                else
                                    checkBoxCaculateSide.Checked = false;

                                checkBoxCaculateSide_CheckedChanged(checkBoxCaculateSide, new EventArgs());
                                checkBoxDoScale_CheckedChanged(checkBoxDoScale, new EventArgs());

                                onShown(null, null);

                                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                                {
                                    currentState.SelectedCalibrationTable.CalibrationTable.CalculateKoef();
                                    currentState.SelectedCalibrationTable.CalibrationTable.SaveTable();

                                    biosTranslated = false;
                                    if (!currentState.PacketsTurnOn) currentState.PacketsTurnOn = true;
                                    TurnOnPackets(null, null);

                                    packetSyncMutex.ReleaseMutex();
                                }
                                else
                                    MessageBox.Show(this, "Не настроить таблицу калибровки для работы." +
                                        Constants.vbCrLf + "Перезагрузите таблицу калибровки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                break;

                            case LastError.Error:

                                MessageBox.Show(this, "Не удалось загрузить таблицу калибровки." +
                                    Constants.vbCrLf + "Возможно данные не корректы в таблице калибровки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                            case LastError.Default:

                                break;

                            default:

                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// сохранить таблицу калибровки в устройство
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вУстройсвоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentState.SelectedCalibrationTable == null)
            {
                MessageBox.Show(this, "Не загруженна конфигурация калибровочных параметров",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (currentState.SelectedCalibrationTable.CalibrationTable == null)
            {
                MessageBox.Show(this, "Не загруженна таблица калибровки",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool blocked = false;
            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;
                    biosTranslated = true;

                    blocked = false;
                    packetSyncMutex.ReleaseMutex();
                }
                else
                {
                    MessageBox.Show(this, "Не удается сохрать таблицу калибровки" + Constants.vbCrLf +
                        "Причина: очень энтенсивный обмен данными", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                SaveForm saver = new SaveForm(bios, currentState);
                if (!currentState.PacketsTurnOn)
                {
                    currentState.PacketsTurnOn = true;
                    TurnOnPackets(null, null);
                }

                saver.ShowDialog(this);

                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;
                    biosTranslated = false;

                    blocked = false;
                    packetSyncMutex.ReleaseMutex();
                }
                else
                {
                    MessageBox.Show(this, "Не удается сохрать таблицу калибровки" + Constants.vbCrLf +
                        "Причина: очень энтенсивный обмен данными", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();

                //currentState.PacketsTurnOn = false;
                //TurnOffPackets(null, null);
            }
        }

        /// <summary>
        /// Вывод графика на форму
        /// </summary>
        /// <param name="calibrationTable">Таблица калибровки которую необходимо показать</param>
        private void ShowCalibrationTableInGraphics(CalibrationTable calibrationTable)
        {
            int k = 0;
            if (currentState.SelectedCalibrationTable != null)
            {
                if (currentState.SelectedCalibrationTable.CalibrationTable != null)
                {
                    if (checkBoxDoScale.Checked && currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count > 3) k = 1;
                }
            }

            Point[] points = new Point[calibrationTable.Parameters.Count - k];
            for (int index = 0; index < calibrationTable.Parameters.Count - k; index++)
            {
                points[index] = new Point(calibrationTable.Parameters[index].Physical,
                    calibrationTable.Parameters[index].Calibrated);
            }
            gr.InsertPoints(points);
        }

        /// <summary>
        /// Вывод таблицы калибровки
        /// </summary>
        /// <param name="calibrationTable">Таблица которую необходимо вывести</param>
        private void ShowCalibrationTableInDataGrid(CalibrationTable calibrationTable)
        {
            dataGridViewCalibrationTable.Rows.Clear();
            for (int i = 0; i < 11; i++)
            {
                DataGridViewRow r = new DataGridViewRow();

                if ((i % 2) == 0) r.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dataGridViewCalibrationTable.Rows.Add(r);
            }

            for (int i = 0; i < currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count; i++)
            {
                dataGridViewCalibrationTable[0, i].Value = (ushort)currentState.SelectedCalibrationTable.CalibrationTable.Parameters[i].Physical;
                dataGridViewCalibrationTable[1, i].Value = (ushort)currentState.SelectedCalibrationTable.CalibrationTable.Parameters[i].Calibrated;
            }
        }

        /// <summary>
        /// закрываем форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TurnOffPackets(null, null);
            currentState.PacketsTurnOn = false;
            //packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex);
        }

        /// <summary>
        /// масштабировать без крайней точки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxDoScale_CheckedChanged(object sender, EventArgs e)
        {
            if (currentState.SelectedCalibrationTable != null)
            {
                if (currentState.SelectedCalibrationTable.CalibrationTable != null)
                {
                    if (currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count > 3)
                    {
                        if (checkBoxDoScale.Checked)
                        {
                            Point[] pts = new Point[currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count - 1];
                            for (int index = 0; index < pts.Length; index++)
                            {
                                pts[index] = new Point(currentState.SelectedCalibrationTable.CalibrationTable.Parameters[index].Physical,
                                    currentState.SelectedCalibrationTable.CalibrationTable.Parameters[index].Calibrated);
                            }
                            gr.InsertPoints(pts);

                            gr.LogicalPixelX = pts[pts.Length - 1].X;
                            gr.LogicalPixelY = pts[pts.Length - 1].Y;

                            gr.CalculateScale();
                            onShown(this, new EventArgs());
                        }
                        else
                        {
                            Point[] pts = new Point[currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count];
                            for (int index = 0; index < pts.Length; index++)
                            {
                                pts[index] = new Point(currentState.SelectedCalibrationTable.CalibrationTable.Parameters[index].Physical,
                                    currentState.SelectedCalibrationTable.CalibrationTable.Parameters[index].Calibrated);
                            }
                            gr.InsertPoints(pts);

                            gr.LogicalPixelX = pts[pts.Length - 1].X;
                            gr.LogicalPixelY = pts[pts.Length - 1].Y;

                            gr.CalculateScale();
                            onShown(this, new EventArgs());
                        }
                    }
                    else
                    {
                        ShowCalibrationTableInGraphics(currentState.SelectedCalibrationTable.CalibrationTable);
                    }
                }
            }
        }

        private void checkBoxCaculateSide_CheckedChanged(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;
                    if (currentState.SelectedCalibrationTable != null)
                    {
                        if (currentState.SelectedCalibrationTable.CalibrationTable != null)
                        {
                            if (((CheckBox)sender).Checked)
                            {
                                currentState.SelectedCalibrationTable.CalibrationTable.CalculateExtremePoints();
                            }
                            else
                                currentState.SelectedCalibrationTable.CalibrationTable.RestoreExtremePoints();

                            currentState.SelectedCalibrationTable.CalibrationTable.CalculateKoef();

                            if (currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count > 0)
                            {
                                gr.LogicalPixelX = currentState.SelectedCalibrationTable.CalibrationTable.Parameters
                                    [currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count - 1].Physical;

                                gr.LogicalPixelY = currentState.SelectedCalibrationTable.CalibrationTable.Parameters
                                    [currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count - 1].Calibrated;

                                gr.CalculateScale();
                            }

                            checkBoxDoScale_CheckedChanged(checkBoxDoScale, new EventArgs());

                            ShowCalibrationTableInDataGrid(currentState.SelectedCalibrationTable.CalibrationTable);
                            ShowCalibrationTableInGraphics(currentState.SelectedCalibrationTable.CalibrationTable);

                            onShown(null, null);
                        }
                    }
                    blocked = false;
                    packetSyncMutex.ReleaseMutex();
                }
                else
                    MessageBox.Show(this, "Не удалось расчитать крайние точки. " +
                        Constants.vbCrLf + "Причина: не удалось получить доступ к таблице калибровке", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// на добавление новой точки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fromTo_Click(object sender, EventArgs e)
        {
            textBoxTotablePhysic.Text = textBoxFromDevicePhysic.Text;
            textBoxToTableCalibrated.Text = textBoxFromDeviceCalibrated.Text;
        }

        /// <summary>
        /// добавить новую точку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insertPoint_Click(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;

                    if (currentState.SelectedCalibrationTable == null) return;
                    if (currentState.SelectedCalibrationTable.CalibrationTable == null) return;
                    if (currentState.SelectedCalibrationTable.CalibrationTable.Parameters == null) return;

                    if (currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count < 11)
                    {
                        ushort physic = 0;
                        ushort calibrated = 0;

                        try
                        {
                            physic = ushort.Parse(textBoxTotablePhysic.Text);
                            calibrated = ushort.Parse(textBoxToTableCalibrated.Text);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(this, "Указанны не верные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (currentState.SelectedCalibrationTable.CalibrationTable.InsertPoint(new Parameter(physic, calibrated)) == LastError.Success)
                        {
                            currentState.SelectedCalibrationTable.CalibrationTable.CalculateKoef();

                            checkBoxCaculateSide_CheckedChanged(checkBoxCaculateSide, new EventArgs());
                            checkBoxDoScale_CheckedChanged(checkBoxDoScale, new EventArgs());

                            ShowCalibrationTableInDataGrid(currentState.SelectedCalibrationTable.CalibrationTable);
                            ShowCalibrationTableInGraphics(currentState.SelectedCalibrationTable.CalibrationTable);
                        }
                        else
                            MessageBox.Show(this, "Данная точка не корректна", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show(this, "Количество точек максимально", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(this, "Не удалось добавить точку. " +
                        Constants.vbCrLf + "Причина: не удалось получить доступ к таблице калибровке", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// удаляем точку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletePoint_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridViewCalibrationTable.SelectedCells[0].RowIndex;
            bool blocked = false;

            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;
                    if (selectedRow != 0 && selectedRow != currentState.SelectedCalibrationTable.CalibrationTable.Parameters.Count - 1)
                    {
                        currentState.SelectedCalibrationTable.CalibrationTable.Parameters.RemoveAt(selectedRow);
                        currentState.SelectedCalibrationTable.CalibrationTable.CalculateKoef();

                        checkBoxCaculateSide_CheckedChanged(checkBoxCaculateSide, new EventArgs());
                        checkBoxDoScale_CheckedChanged(checkBoxDoScale, new EventArgs());

                        ShowCalibrationTableInDataGrid(currentState.SelectedCalibrationTable.CalibrationTable);
                    }
                }
                else
                    MessageBox.Show(this, "Не удалось удалить точку." + Constants.vbCrLf +
                        "Причина: Интенсивный обмен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// сброс таблицы калибровки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationReset_Click(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;
                    if (currentState.SelectedCalibrationTable != null)
                    {
                        blocked = true;

                        currentState.SelectedCalibrationTable.CalibrationTable.ResetTable();
                        currentState.SelectedCalibrationTable.CalibrationTable.CalculateKoef();

                        ShowCalibrationTableInDataGrid(currentState.SelectedCalibrationTable.CalibrationTable);

                        checkBoxCaculateSide_CheckedChanged(checkBoxCaculateSide, new EventArgs());
                        checkBoxDoScale_CheckedChanged(checkBoxDoScale, new EventArgs());

                        ShowCalibrationTableInGraphics(currentState.SelectedCalibrationTable.CalibrationTable);
                        onShown(this, new EventArgs());
                    }
                    else
                        MessageBox.Show(this, "Не удалось сбросить таблицу." + Constants.vbCrLf +
                            "Причина: Интенсивный обмен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// возврат к исходной таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationLast_Click(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;

                    if (currentState.Eprom == null) return;
                    if (currentState.SelectedCalibrationTable == null) return;
                    if (currentState.SelectedCalibrationTable.CalibrationTable == null) return;

                    currentState.SelectedCalibrationTable.CalibrationTable.RestoreTable();
                    currentState.SelectedCalibrationTable.CalibrationTable.CalculateKoef();

                    ShowCalibrationTableInDataGrid(currentState.SelectedCalibrationTable.CalibrationTable);

                    checkBoxCaculateSide_CheckedChanged(checkBoxCaculateSide, new EventArgs());
                    checkBoxDoScale_CheckedChanged(checkBoxDoScale, new EventArgs());

                    ShowCalibrationTableInGraphics(currentState.SelectedCalibrationTable.CalibrationTable);
                    onShown(this, new EventArgs());
                }
                else
                    MessageBox.Show(this, "Не удалось вернуться к исходной таблице." + Constants.vbCrLf +
                        "Причина: Интенсивный обмен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// запись в устройство
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationWrite_Click(object sender, EventArgs e)
        {
            вУстройсвоToolStripMenuItem_Click(null, null);
        }

        /// <summary>
        /// загружаем с файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadFromFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                FileFormat format = FileFormat.EF1TXT;
                switch (openFileDialog.FilterIndex)
                {
                    case 1:

                        format = FileFormat.EF1TXT;
                        break;

                    case 2:

                        format = FileFormat.EF2XMLOLD;
                        break;

                    case 3:

                        format = FileFormat.EF2XML;
                        break;
                }

                bool blocked = false;
                try
                {
                    if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                    {
                        blocked = true;

                        currentState.PacketsTurnOn = false;
                        TurnOffPackets(null, null);

                        currentState.Eprom = bios.LoadEpromFromFile(openFileDialog.FileName, format);
                        if (currentState.Eprom == null)
                        {
                            MessageBox.Show(this, "Произошла не предвиденная ошибка!" +
                                Constants.vbCrLf + "не удалось загрузить таблицу описателей калибруемых параметров",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            currentState.CalibrationTableHandles = bios.CreateCalibrationTableHandles(currentState.Eprom);
                            if (currentState.CalibrationTableHandles == null)
                            {
                                MessageBox.Show(this, "Данные в EPROM устройстве не корректны!" +
                                    Constants.vbCrLf + "Не удалось выделить таблицу описателей калибровочных параметров",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                currentState.Eprom = null;
                            }
                        }

                        gr.ResetPoint();
                        gr.ResetPoints();

                        gr.ResetScale();

                        ClearDataGrid();
                        onShown(this, new EventArgs());
                    }
                    else
                        MessageBox.Show(this, "Не удалось загрузить из файла." + Constants.vbCrLf +
                            "Причина: Интенсивный обмен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Не удалось загрузить файл. Возможно файл не соответствует формату или поврежден",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    if (blocked) packetSyncMutex.ReleaseMutex();
                }
            }
        }

        private void вФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;
                    
                    if (currentState.Eprom == null) return;
                    if (currentState.SelectedCalibrationTable == null)
                    {
                        MessageBox.Show(this, "Не выбрана таблица калибровки", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (currentState.SelectedCalibrationTable.CalibrationTable == null) return;
                    
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        bios.SaveEpromToFile(saveFileDialog.FileName, currentState.SelectedCalibrationTable.CalibrationTable, currentState.Eprom);
                    }
                }
                else
                    MessageBox.Show(this, "Не удалось сохранить в файл." + Constants.vbCrLf +
                        "Причина: Интенсивный обмен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// началось редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCalibrationTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {   
            needCorrect = false;
            correctP = false;

            if (e.ColumnIndex == 0) correctP = true;
            oldValue = dataGridViewCalibrationTable[e.ColumnIndex, e.RowIndex].Value;
        }

        /// <summary>
        /// закончилось редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCalibrationTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
            dataGridViewCalibrationTable[e.ColumnIndex, e.RowIndex].Value = newValue;

            bool blocked = false;
            try
            {
                if (packetSyncMutex.WaitOne(defaultTimeWaitOnPacketMutex))
                {
                    blocked = true;

                    if (currentState.SelectedCalibrationTable == null) return;
                    if (currentState.SelectedCalibrationTable.CalibrationTable == null) return;
                    if (currentState.SelectedCalibrationTable.CalibrationTable.Parameters == null) return;

                    if (correctP)
                    {
                        currentState.SelectedCalibrationTable.CalibrationTable.Parameters[e.RowIndex].Physical = ushort.Parse(newValue.ToString());
                    }
                    else
                    {
                        currentState.SelectedCalibrationTable.CalibrationTable.Parameters[e.RowIndex].Calibrated = ushort.Parse(newValue.ToString());
                    }

                    ShowCalibrationTableInGraphics(currentState.SelectedCalibrationTable.CalibrationTable);
                    ShowCalibrationTableInDataGrid(currentState.SelectedCalibrationTable.CalibrationTable);

                    checkBoxCaculateSide_CheckedChanged(checkBoxCaculateSide, new EventArgs());
                    checkBoxDoScale_CheckedChanged(checkBoxDoScale, new EventArgs());

                    onShown(null, null);
                }
                else
                    MessageBox.Show(this, "Не удалось отредактировать точку." + Constants.vbCrLf +
                        "Причина: Интенсивный обмен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            finally
            {
                if (blocked) packetSyncMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// парсим
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCalibrationTable_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (Type.GetTypeCode(e.Value.GetType()) == TypeCode.String)
            {
                try
                {
                    ushort nValue = ushort.Parse(e.Value.ToString());

                    ushort last = ushort.Parse(dataGridViewCalibrationTable[e.ColumnIndex, e.RowIndex - 1].Value.ToString());
                    ushort next = ushort.Parse(dataGridViewCalibrationTable[e.ColumnIndex, e.RowIndex + 1].Value.ToString());

                    if (nValue > last && nValue < next)
                    {
                        e.ParsingApplied = true;
                        newValue = e.Value;

                        needCorrect = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "Данное число не корректно",
                            "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        newValue = oldValue;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Введенное значение не является допустимым числом");
                }
            }
        }
    }

    delegate void TextInsert(TextBox list, string val);
}
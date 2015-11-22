using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _3d
{
    public partial class two11xuan5 : Form
    {
        private int dingWeiZuHeTextCtrlNums = 0;//统计一共多少个定位组合textbox
        private int dingWeiShaZuHeTextCtrlNums = 0;//统计一共多少个定位杀组合textbox
        private List<string> allDataList = new List<string>();
        private static string[] zhiShuArray = new string[] { "01", "02", "03", "05", "07", "11" };//质数
        private static string[] heShuArray = new string[] { "04", "06", "08", "09", "10" };//合数
        private static string[] danShuArray = new string[] { "01", "03", "05", "07", "09", "11" };//单数
        private static string[] shuangShuArray = new string[] { "02", "04", "06", "08", "10" };//双数

        public two11xuan5()
        {
            InitializeComponent();
            Tools.SetGroupBoxPaintAll(this.Controls);
        }

        private void two11xuan5_Load(object sender, EventArgs e)
        {
            biXiaLiangMaSetCtrl();
        }

        /// <summary>
        /// main界面“计算”按钮调用方法，用于发送11选5数据
        /// </summary>
        /// <returns></returns>
        public List<string> sendData(List<string> allData)
        {
            List<string> li = new List<string>();
            li.AddRange(biXiaLiangMa(allData));
            li.Sort();//将结果集排序
            return li.Distinct().ToList();
        }

        /// <summary>
        /// 必下两码 
        /// </summary>
        private List<string> biXiaLiangMa(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in biXiaLiangMaGpb.Controls)
            {
                if (ctl is TextBox && ctl.Name.IndexOf("dwZuHe_") > -1)
                {
                    TextBox tb = ctl as TextBox;
                    if (!string.IsNullOrEmpty(tb.Text))
                    {
                        oneNums.Add(tb.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return biShaLiangMa(allData);
            }

            foreach (string oneLineData in allData)
            {
                foreach (string oneNum in oneNums)
                {
                    if (oneNum.Length == 4)
                    {
                        string biXia1 = oneNum.Substring(0, 2);
                        string biXia2 = oneNum.Substring(2, 2);
                        if (oneLineData.IndexOf(biXia1) > -1 && oneLineData.IndexOf(biXia2) > -1)
                        {
                            resData.Add(oneLineData);
                        }
                    }
                }
            }
            return biShaLiangMa(resData);
        }

        /// <summary>
        /// 必杀两码 
        /// </summary>
        private List<string> biShaLiangMa(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in biXiaLiangMaGpb.Controls)
            {
                if (ctl is TextBox && ctl.Name.IndexOf("shaZuHe_") > -1)
                {
                    TextBox tb = ctl as TextBox;
                    if (!string.IsNullOrEmpty(tb.Text))
                    {
                        oneNums.Add(tb.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return heZhi(allData);
            }

            resData.AddRange(allData);
            foreach (string oneLineData in allData)
            {
                foreach (string oneNum in oneNums)
                {
                    if (oneNum.Length == 4)
                    {
                        string biXia1 = oneNum.Substring(0, 2);
                        string biXia2 = oneNum.Substring(2, 2);
                        if (oneLineData.IndexOf(biXia1) > -1 && oneLineData.IndexOf(biXia2) > -1)
                        {
                            resData.Remove(oneLineData);
                        }
                    }
                }
            }
            return heZhi(resData);
        }

        /// <summary>
        /// 合值 
        /// </summary>
        private List<string> heZhi(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in heZhiGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return sumZhi(allData);
            }

            foreach (string oneLineData in allData)
            {
                string[] oneLineDatas = oneLineData.Split(new string[] { one11xuan5.fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                int no_1 = Convert.ToInt16(oneLineDatas[0]);
                int no_2 = Convert.ToInt16(oneLineDatas[1]);
                int no_3 = Convert.ToInt16(oneLineDatas[2]);
                int no_4 = Convert.ToInt16(oneLineDatas[3]);
                int no_5 = Convert.ToInt16(oneLineDatas[4]);

                string sumNum = no_1 + no_2 + no_3 + no_4 + no_5+"";
                foreach (string oneNum in oneNums)
                {
                    sumNum = sumNum.Substring(sumNum.Length -1);
                    if (sumNum.Equals(oneNum))
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return sumZhi(resData);
        }

        /// <summary>
        /// 和值
        /// </summary>
        private List<string> sumZhi(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in sumZhiGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return kuaDu(allData);
            }

            foreach (string oneLineData in allData)
            {
                string[] oneLineDatas = oneLineData.Split(new string[] { one11xuan5.fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                int no_1 = Convert.ToInt16(oneLineDatas[0]);
                int no_2 = Convert.ToInt16(oneLineDatas[1]);
                int no_3 = Convert.ToInt16(oneLineDatas[2]);
                int no_4 = Convert.ToInt16(oneLineDatas[3]);
                int no_5 = Convert.ToInt16(oneLineDatas[4]);

                int sumNum = no_1 + no_2 + no_3 + no_4 + no_5;

                foreach (string oneNum in oneNums)
                {
                    if (sumNum == Convert.ToInt16(oneNum))
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return kuaDu(resData);
        }

        /// <summary>
        /// 跨度
        /// </summary>
        private List<string> kuaDu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in kuaDuGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return fanBianQiu(allData);
            }

            foreach (string oneLineData in allData)
            {
                string[] oneLineDatas = oneLineData.Split(new string[] { one11xuan5.fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                int no_1 = Convert.ToInt16(oneLineDatas[0]);
                int no_2 = Convert.ToInt16(oneLineDatas[1]);
                int no_3 = Convert.ToInt16(oneLineDatas[2]);
                int no_4 = Convert.ToInt16(oneLineDatas[3]);
                int no_5 = Convert.ToInt16(oneLineDatas[4]);

                foreach (string oneNum in oneNums)
                {
                    if (no_5 - no_1 == Convert.ToInt16(oneNum))
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return fanBianQiu(resData);
        }

        /// <summary>
        /// 反边球距离
        /// </summary>
        private List<string> fanBianQiu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in fanBianQiuGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return zuiDaLinMaKuaJu(allData);
            }

            foreach (string oneLineData in allData)
            {
                string[] oneLineDatas = oneLineData.Split(new string[] { one11xuan5.fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                int no_1 = Convert.ToInt16(oneLineDatas[0]);
                int no_2 = Convert.ToInt16(oneLineDatas[1]);
                int no_3 = Convert.ToInt16(oneLineDatas[2]);
                int no_4 = Convert.ToInt16(oneLineDatas[3]);
                int no_5 = Convert.ToInt16(oneLineDatas[4]);
                int leftSpaceNums = no_1 - 1;//左边空格数
                int rightSpaceNums = 11 - no_5;//右边空格数

                foreach (string oneNum in oneNums)
                {
                    if (leftSpaceNums + rightSpaceNums == Convert.ToInt16(oneNum))
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return zuiDaLinMaKuaJu(resData);
        }

        /// <summary>
        /// 最大邻码跨距
        /// </summary>
        private List<string> zuiDaLinMaKuaJu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in zdlmkjGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return linMaHe(allData);
            }

            foreach (string oneLineData in allData)
            {
                string[] oneLineDatas = oneLineData.Split(new string[] { one11xuan5.fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string oneNum in oneNums)
                {
                    int i = 0;
                    int maxShort = 0;//相邻两数之间最大的差
                    while (i < oneLineDatas.Length - 1)
                    {
                        int zdlmkjSum = Math.Abs(Convert.ToInt16(oneLineDatas[i]) - Convert.ToInt16(oneLineDatas[i + 1]));
                        if (zdlmkjSum>maxShort)
                        {
                            maxShort = zdlmkjSum;
                        }
                        i++;
                    }

                    if (maxShort.ToString().Equals(oneNum))
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return linMaHe(resData);
        }

        /// <summary>
        /// 临码和
        /// </summary>
        private List<string> linMaHe(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in linMaHeGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return bianLinHe(allData);
            }

            foreach (string oneLineData in allData)
            {
                string[] oneLineDatas = oneLineData.Split(new string[] { one11xuan5.fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string oneNum in oneNums)
                {
                    int i = 0;
                    int shortNum = 0;//最大数和最小数之间的空格数
                    while (i < oneLineDatas.Length - 1)
                    {
                        int zdlmkjSum = Math.Abs(Convert.ToInt16(oneLineDatas[i + 1]) - Convert.ToInt16(oneLineDatas[i]) - 1);
                        if (zdlmkjSum!=0)
                        {
                            shortNum += zdlmkjSum;
                        }
                        i++;
                    }

                    if (shortNum.ToString().Equals(oneNum))
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return bianLinHe(resData);
        }

        /// <summary>
        /// 边临和
        /// </summary>
        private List<string> bianLinHe(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in bianLinHeGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return renYiLiangMaCha(allData);
            }

            foreach (string oneLineData in allData)
            {
                string[] oneLineDatas = oneLineData.Split(new string[] { one11xuan5.fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                int no_1 = Convert.ToInt16(oneLineDatas[0]);
                int no_2 = Convert.ToInt16(oneLineDatas[1]);
                int no_3 = Convert.ToInt16(oneLineDatas[2]);
                int no_4 = Convert.ToInt16(oneLineDatas[3]);
                int no_5 = Convert.ToInt16(oneLineDatas[4]);
                int leftSpaceNums = no_1 - 1;//左边空格数
                int rightSpaceNums = 11 - no_5;//右边空格数
                int bianLinSum = leftSpaceNums + rightSpaceNums;

                foreach (string oneNum in oneNums)
                {
                    int i = 0;
                    int maxShort = 0;//相邻两数之间最大的差
                    while (i < oneLineDatas.Length - 1)
                    {
                        int zdlmkjSum = Math.Abs(Convert.ToInt16(oneLineDatas[i]) - Convert.ToInt16(oneLineDatas[i + 1]));
                        if (zdlmkjSum > maxShort)
                        {
                            maxShort = zdlmkjSum;
                        }
                        i++;
                    }

                    if ((maxShort + bianLinSum).ToString().Equals(oneNum))
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return renYiLiangMaCha(resData);
        }

        /// <summary>
        /// 任意两码差
        /// </summary>
        private List<string> renYiLiangMaCha(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in renYiLiangMaChaGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (oneNums == null || oneNums.Count == 0)
            {
                return longTouFengWei(allData);
            }

            foreach (string oneLineData in allData)
            {
                string[] srcNums = oneLineData.Split(new string[] { one11xuan5.fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                int srcNumsCount = srcNums.Length;
                int srcNumsCountLessOne = srcNumsCount - 1;
                int tempSum = 0;

                foreach (string oneNum in oneNums)
                {
                    int shortNum = Convert.ToInt16(oneNum);
                    for (int i = 0; i < srcNumsCount; i++)
                    {
                        string currStr = srcNums[i];
                        int temp_i = i;

                        //组成每一个“出3位数”
                        while (temp_i < srcNumsCountLessOne)
                        {
                            temp_i++;
                            string nextStr = srcNums[temp_i];
                            int temp_i2 = temp_i;

                            while (temp_i2 < srcNumsCountLessOne)
                            {
                                temp_i2++;
                                string next2Str = srcNums[temp_i2];
                                int temp_i3 = temp_i2;

                                while (temp_i3 < srcNumsCountLessOne)
                                {
                                    temp_i3++;
                                    string next3Str = srcNums[temp_i3];
                                    int temp_i4 = temp_i3;

                                    while (temp_i4 < srcNumsCountLessOne)
                                    {
                                        temp_i4++;
                                        string next4Str = srcNums[temp_i4];
                                        tempSum = Convert.ToInt16(next4Str) - Convert.ToInt16(next3Str);
                                        if (oneNum.Equals(tempSum.ToString()))
                                        {
                                            resData.Add(oneLineData);
                                        }
                                    }

                                    tempSum = Convert.ToInt16(next3Str) - Convert.ToInt16(next2Str);
                                    if (oneNum.Equals(tempSum.ToString()))
                                    {
                                        resData.Add(oneLineData);
                                    }
                                }

                                tempSum = Convert.ToInt16(next2Str) - Convert.ToInt16(nextStr);
                                if (oneNum.Equals(tempSum.ToString()))
                                {
                                    resData.Add(oneLineData);
                                }
                            }

                            tempSum = Convert.ToInt16(nextStr) - Convert.ToInt16(currStr);
                            if (oneNum.Equals(tempSum.ToString()))
                            {
                                resData.Add(oneLineData);
                            }
                        }
                    }
                }
            }
            return longTouFengWei(resData);
        }

        /// <summary>
        /// 龙头凤尾
        /// </summary>
        private List<string> longTouFengWei(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> longTouNums = new List<string>();
            List<string> fengWeiNums = new List<string>();
            foreach (Control ctl in longTouGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        longTouNums.Add(ctl.Text);
                    }
                }
            }

            foreach (Control ctl in fengWeiGpb.Controls)
            {
                if (ctl is CheckBox)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        fengWeiNums.Add(ctl.Text);
                    }
                }
            }

            if ((longTouNums == null || longTouNums.Count == 0) && (fengWeiNums == null || fengWeiNums.Count == 0))
            {
                return allData;
            }

            foreach (string oneLineData in allData)
            {
                string[] oneLineDatas = oneLineData.Split(new string[] { one11xuan5.fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                string longTou = oneLineDatas[0];
                string fengWei = oneLineDatas[4];

                if (longTouNums.Contains("单"))
                {
                    if (danShuArray.Contains(longTou))
                    {
                        resData.Add(oneLineData);
                    }
                }

                if (longTouNums.Contains("双"))
                {
                    if (shuangShuArray.Contains(longTou))
                    {
                        resData.Add(oneLineData);
                    }
                }

                if (longTouNums.Contains("质"))
                {
                    if (zhiShuArray.Contains(longTou))
                    {
                        resData.Add(oneLineData);
                    }
                }

                if (longTouNums.Contains("合"))
                {
                    if (heShuArray.Contains(longTou))
                    {
                        resData.Add(oneLineData);
                    }
                }

                if (fengWeiNums.Contains("单"))
                {
                    if (danShuArray.Contains(fengWei))
                    {
                        resData.Add(oneLineData);
                    }
                }

                if (fengWeiNums.Contains("双"))
                {
                    if (shuangShuArray.Contains(fengWei))
                    {
                        resData.Add(oneLineData);
                    }
                }

                if (fengWeiNums.Contains("质"))
                {
                    if (zhiShuArray.Contains(fengWei))
                    {
                        resData.Add(oneLineData);
                    }
                }

                if (fengWeiNums.Contains("合"))
                {
                    if (heShuArray.Contains(fengWei))
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return resData;
        }

        /// <summary>
        /// 必下两码组合的TextBox_Change事件绑定
        /// </summary>
        private void biXiaLiangMaSetCtrl()
        {
            foreach (Control txtCtrl in this.biXiaLiangMaGpb.Controls)
            {
                if (txtCtrl is TextBox)
                {
                    if (txtCtrl.Name.IndexOf("dwZuHe_") > -1)
                    {
                        dingWeiZuHeTextCtrlNums++;
                    }
                    if (txtCtrl.Name.IndexOf("shaZuHe_") > -1)
                    {
                        dingWeiShaZuHeTextCtrlNums++;
                    }
                    txtCtrl.TextChanged += new System.EventHandler(this.biXiaLiangMa_textChanged);

                    //除了第一个框以外都先变灰
                    if (txtCtrl.Name.IndexOf("1") < 0)
                    {
                        txtCtrl.Enabled = false;
                    }
                }
            }
        }

        private void biXiaLiangMa_textChanged(object sender, EventArgs e)
        {
            TextBox txt = ((TextBox)sender);
            string txtNameLeft = txt.Name.Split('_')[0];
            int txtNumber = Convert.ToInt16(txt.Name.Split('_')[1]);
            if (txtNumber < dingWeiZuHeTextCtrlNums)
            {
                if (txt.Text.Length == txt.MaxLength)
                {
                    string nextTxtBoxName = txtNameLeft + "_" + (txtNumber + 1);
                    foreach (Control txtCtrl in this.biXiaLiangMaGpb.Controls)
                    {
                        if (txtCtrl.Name.Equals(nextTxtBoxName))
                        {
                            txtCtrl.Enabled = true;
                            txtCtrl.Focus();
                        }
                    }
                }
                if (txt.Text.Length < txt.MaxLength)
                {
                    string nextTxtBoxName = txtNameLeft + "_" + (txtNumber + 1);
                    foreach (Control txtCtrl in this.biXiaLiangMaGpb.Controls)
                    {
                        if (txtCtrl.Name.Equals(nextTxtBoxName))
                        {
                            txtCtrl.Enabled = false;
                            txtCtrl.Text = "";
                            txt.Focus();
                        }
                    }
                }
            }
        }

        //清空页面上的Checkbox
        public void clearCheckbox()
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is GroupBox)
                {
                    foreach (Control ctls in ctl.Controls)
                    {
                        if (ctls is CheckBox)
                        {
                            (ctls as CheckBox).Checked = false;
                        }
                        if (ctls is TextBox)
                        {
                            (ctls as TextBox).Text = "";
                        }
                        if (ctls is GroupBox)
                        {
                            foreach (Control ctlls in ctls.Controls)
                            {
                                if (ctlls is CheckBox)
                                {
                                    (ctlls as CheckBox).Checked = false;
                                }
                                if (ctlls is TextBox)
                                {
                                    (ctlls as TextBox).Text = "";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void touDanCbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = ((CheckBox)sender);
            if (cb.Checked == true)
            {
                touShuangCbx.Checked = false;
                touShuangCbx.Enabled = false;
            }
            else
            {
                touShuangCbx.Enabled = true;
            }
        }

        private void touShuangCbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = ((CheckBox)sender);
            if (cb.Checked == true)
            {
                touDanCbx.Checked = false;
                touDanCbx.Enabled = false;
            }
            else
            {
                touDanCbx.Enabled = true;
            }
        }

        private void touZhiCbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = ((CheckBox)sender);
            if (cb.Checked == true)
            {
                touHeCbx.Checked = false;
                touHeCbx.Enabled = false;
            }
            else
            {
                touHeCbx.Enabled = true;
            }
        }

        private void touHeCbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = ((CheckBox)sender);
            if (cb.Checked == true)
            {
                touZhiCbx.Checked = false;
                touZhiCbx.Enabled = false;
            }
            else
            {
                touZhiCbx.Enabled = true;
            }
        }

        private void weiDanCbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = ((CheckBox)sender);
            if (cb.Checked == true)
            {
                weiShuangCbx.Checked = false;
                weiShuangCbx.Enabled = false;
            }
            else
            {
                weiShuangCbx.Enabled = true;
            }
        }

        private void weiShuangCbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = ((CheckBox)sender);
            if (cb.Checked == true)
            {
                weiDanCbx.Checked = false;
                weiDanCbx.Enabled = false;
            }
            else
            {
                weiDanCbx.Enabled = true;
            }
        }

        private void weiZhiCbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = ((CheckBox)sender);
            if (cb.Checked == true)
            {
                weiHeCbx.Checked = false;
                weiHeCbx.Enabled = false;
            }
            else
            {
                weiHeCbx.Enabled = true;
            }
        }

        private void weiHeCbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = ((CheckBox)sender);
            if (cb.Checked == true)
            {
                weiZhiCbx.Checked = false;
                weiZhiCbx.Enabled = false;
            }
            else
            {
                weiZhiCbx.Enabled = true;
            }
        }

        /// <summary>
        /// 清空单个框体里面的单选框勾选
        /// </summary>
        /// <param name="ctrls"></param>
        private void clearSingleCheckbox(Control ctrls)
        {
            foreach (Control cbx in ctrls.Controls)
            {
                if (cbx is CheckBox)
                {
                    (cbx as CheckBox).Checked = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.kuaDuGpb);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.zdlmkjGpb);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.fanBianQiuGpb);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.bianLinHeGpb);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.linMaHeGpb);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.heZhiGpb);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.renYiLiangMaChaGpb);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.sumZhiGpb);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dwZuHe_1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            shaZuHe_1.Text = "";
        }
    }
}

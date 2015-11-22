using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace _3d
{
    public partial class one11xuan5 : Form
    {
        public static string fenGeFu = " ";//当前分隔符
        private static string fenGeFuOld = " ";//上一个分隔符
        private static int countOneHaoZuCbxOld = 0;
        private static int countTwoHaoZuCbxOld = 0;
        private static int countThreeHaoZuCbxOld = 0;
        private static int countFourHaoZuCbxOld = 0;
        private string allDataStr = Properties.Resources._11xuan5;
        private List<string> allDataList = new List<string>();
        private two11xuan5 o11_2 = new two11xuan5();

        private static string[] ouShuArray = new string[] { "02", "04", "06", "08", "10" };//偶数
        private static string[] zhiShuArray = new string[] { "01", "02", "03", "05", "07", "11" };//质数
        private static string[] heShuArray = new string[] { "04", "06", "08", "09", "10" };//合数
        private static string[] xiaoShuArray = new string[] { "01", "02", "03", "04", "05" };//小数
        private static string[] daShuArray = new string[] { "06", "07", "08", "09", "10", "11" };//大数
        private static string[] niWeiArray = new string[] { "01", "02", "03", "04", "05" };//逆位
        private static string[] zhengWeiArray = new string[] { "07", "08", "09", "10", "11" };//正位
        private static string[] pingHengLeftArray = new string[] { "01", "02", "03", "04", "05" };//平衡指数左
        private static string[] pingHengRightArray = new string[] { "07", "08", "09", "10", "11" };//平衡指数右
        private static string[] lianHaoLeftArray = new string[] { "01" + fenGeFu + "02", "02" + fenGeFu + "03", "03" + fenGeFu + "04", "04" + fenGeFu + "05", "05" + fenGeFu + "06" };//连号轨迹左
        private static string[] lianHaoRightArray = new string[] { "06" + fenGeFu + "07", "07" + fenGeFu + "08", "08" + fenGeFu + "09", "09" + fenGeFu + "10", "10" + fenGeFu + "11" };//连号轨迹右

        public one11xuan5()
        {
            InitializeComponent();
            getMainData();
            this.fenGeCombx.SelectedIndex = 0;
            this.timer1.Interval = 1;
            this.timer1.Enabled = true;
            this.timer2.Interval = 1;
            this.timer1.Enabled = true;

            Tools.SetGroupBoxPaintAll(this.Controls);
        }

        /// <summary>
        /// main界面“计算”按钮调用方法，用于发送11选5数据
        /// </summary>
        /// <returns></returns>
        public List<string> sendData()
        {
            List<string> li = new List<string>();
            li.AddRange(yiHaoZu());
            li.Sort();//将结果集排序
            return li;
        }

        /// <summary>
        /// 生成主数据
        /// </summary>
        private void getMainData()
        {
            //分隔符替换
            allDataStr = allDataStr.Replace(fenGeFuOld, fenGeFu);
            fenGeFuOld = fenGeFu;//将当前设置的分隔符赋值给老的，以便于下次替换

            //将462注主数据导入List中
            allDataList.Clear();
            string[] allDataStrs = allDataStr.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string oneStr in allDataStrs){
                allDataList.Add(oneStr);
            }
        }

        /// <summary>
        /// 一号组运算
        /// </summary>
        /// <returns></returns>
        private List<string> yiHaoZu()
        {
            string oneNum = "";//所选数字
            string oneZu = "";//所出个数
            foreach (Control ctl in oneZuGpb.Controls)
            {
                if (ctl is CheckBox && (ctl as CheckBox).Checked == true)
                {
                    if (ctl.Name.Contains("oneNum"))
                    {
                        oneNum += ctl.Text + ",";
                    }
                    if (ctl.Name.Contains("oneZu"))
                    {
                        oneZu += ctl.Text + ",";
                    }
                }
            }

            //如果没勾选号码或者所出个数则返回全数据
            if (oneNum.Length == 0 || oneZu.Length == 0)
            {
                return erHaoZu(allDataList);
            }

            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            oneNum = oneNum.Substring(0, oneNum.Length - 1);
            oneZu = oneZu.Substring(0, oneZu.Length - 1);
            oneNums.AddRange(oneNum.Split(','));
            oneNums.Sort();

            if (oneZu.IndexOf("0") > -1)
            {
                resData.AddRange(calcHaoMaZuFor0(allDataList, oneNums));
            }

            if (oneZu.IndexOf("1") > -1)
            {
                resData.AddRange(calcHaoMaZuFor1(allDataList, oneNums, 1));
            }
            
            if (oneZu.IndexOf("2") > -1)
            {
                resData.AddRange(calcHaoMaZuFor2(allDataList, oneNums, 2));
            }
            
            if (oneZu.IndexOf("3") > -1)
            {
                resData.AddRange(calcHaoMaZuFor3(allDataList, oneNums, 3));
            }
            
            if (oneZu.IndexOf("4") > -1)
            {
                resData.AddRange(calcHaoMaZuFor4(allDataList, oneNums, 4));
            }
            
            if (oneZu.IndexOf("5") > -1)
            {
                resData.AddRange(calcHaoMaZuFor5(allDataList, oneNums, 5));
            }

            return erHaoZu(resData);
        }

        /// <summary>
        /// 二号组运算
        /// </summary>
        /// <returns></returns>
        private List<string> erHaoZu(List<string> allData)
        {
            string oneNum = "";//所选数字
            string oneZu = "";//所出个数
            foreach (Control ctl in twoZuGpb.Controls)
            {
                if (ctl is CheckBox && (ctl as CheckBox).Checked == true)
                {
                    if (ctl.Name.Contains("twoNum"))
                    {
                        oneNum += ctl.Text + ",";
                    }
                    if (ctl.Name.Contains("twoZu"))
                    {
                        oneZu += ctl.Text + ",";
                    }
                }
            }

            //如果没勾选号码或者所出个数则返回全数据
            if (oneNum.Length == 0 || oneZu.Length == 0)
            {
                return sanHaoZu(allData);
            }

            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            oneNum = oneNum.Substring(0, oneNum.Length - 1);
            oneZu = oneZu.Substring(0, oneZu.Length - 1);
            oneNums.AddRange(oneNum.Split(','));
            oneNums.Sort();

            if (oneZu.IndexOf("0") > -1)
            {
                resData.AddRange(calcHaoMaZuFor0(allData, oneNums));
            }

            if (oneZu.IndexOf("1") > -1)
            {
                resData.AddRange(calcHaoMaZuFor1(allData, oneNums, 1));
            }

            if (oneZu.IndexOf("2") > -1)
            {
                resData.AddRange(calcHaoMaZuFor2(allData, oneNums, 2));
            }

            if (oneZu.IndexOf("3") > -1)
            {
                resData.AddRange(calcHaoMaZuFor3(allData, oneNums, 3));
            }

            if (oneZu.IndexOf("4") > -1)
            {
                resData.AddRange(calcHaoMaZuFor4(allData, oneNums, 4));
            }

            if (oneZu.IndexOf("5") > -1)
            {
                resData.AddRange(calcHaoMaZuFor5(allData, oneNums, 5));
            }

            return sanHaoZu(resData);
        }

        /// <summary>
        /// 三号组运算
        /// </summary>
        /// <returns></returns>
        private List<string> sanHaoZu(List<string> allData)
        {
            string oneNum = "";//所选数字
            string oneZu = "";//所出个数
            foreach (Control ctl in threeZuGpb.Controls)
            {
                if (ctl is CheckBox && (ctl as CheckBox).Checked == true)
                {
                    if (ctl.Name.Contains("threeNum"))
                    {
                        oneNum += ctl.Text + ",";
                    }
                    if (ctl.Name.Contains("threeZu"))
                    {
                        oneZu += ctl.Text + ",";
                    }
                }
            }

            //如果没勾选号码或者所出个数则返回全数据
            if (oneNum.Length == 0 || oneZu.Length == 0)
            {
                return siHaoZu(allData);
            }

            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            oneNum = oneNum.Substring(0, oneNum.Length - 1);
            oneZu = oneZu.Substring(0, oneZu.Length - 1);
            oneNums.AddRange(oneNum.Split(','));
            oneNums.Sort();

            if (oneZu.IndexOf("0") > -1)
            {
                resData.AddRange(calcHaoMaZuFor0(allData, oneNums));
            }

            if (oneZu.IndexOf("1") > -1)
            {
                resData.AddRange(calcHaoMaZuFor1(allData, oneNums, 1));
            }

            if (oneZu.IndexOf("2") > -1)
            {
                resData.AddRange(calcHaoMaZuFor2(allData, oneNums, 2));
            }

            if (oneZu.IndexOf("3") > -1)
            {
                resData.AddRange(calcHaoMaZuFor3(allData, oneNums, 3));
            }

            if (oneZu.IndexOf("4") > -1)
            {
                resData.AddRange(calcHaoMaZuFor4(allData, oneNums, 4));
            }

            if (oneZu.IndexOf("5") > -1)
            {
                resData.AddRange(calcHaoMaZuFor5(allData, oneNums, 5));
            }

            return siHaoZu(resData);
        }

        /// <summary>
        /// 四号组运算
        /// </summary>
        /// <returns></returns>
        private List<string> siHaoZu(List<string> allData)
        {
            string oneNum = "";//所选数字
            string oneZu = "";//所出个数
            foreach (Control ctl in fourZuGpb.Controls)
            {
                if (ctl is CheckBox && (ctl as CheckBox).Checked == true)
                {
                    if (ctl.Name.Contains("fourNum"))
                    {
                        oneNum += ctl.Text + ",";
                    }
                    if (ctl.Name.Contains("fourZu"))
                    {
                        oneZu += ctl.Text + ",";
                    }
                }
            }

            //如果没勾选号码或者所出个数则返回全数据
            if (oneNum.Length == 0 || oneZu.Length == 0)
            {
                return ouShuGeShu(allData);
            }

            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            oneNum = oneNum.Substring(0, oneNum.Length - 1);
            oneZu = oneZu.Substring(0, oneZu.Length - 1);
            oneNums.AddRange(oneNum.Split(','));
            oneNums.Sort();

            if (oneZu.IndexOf("0") > -1)
            {
                resData.AddRange(calcHaoMaZuFor0(allData, oneNums));
            }

            if (oneZu.IndexOf("1") > -1)
            {
                resData.AddRange(calcHaoMaZuFor1(allData, oneNums, 1));
            }

            if (oneZu.IndexOf("2") > -1)
            {
                resData.AddRange(calcHaoMaZuFor2(allData, oneNums, 2));
            }

            if (oneZu.IndexOf("3") > -1)
            {
                resData.AddRange(calcHaoMaZuFor3(allData, oneNums, 3));
            }

            if (oneZu.IndexOf("4") > -1)
            {
                resData.AddRange(calcHaoMaZuFor4(allData, oneNums, 4));
            }

            if (oneZu.IndexOf("5") > -1)
            {
                resData.AddRange(calcHaoMaZuFor5(allData, oneNums, 5));
            }

            return ouShuGeShu(resData);
        }

        /// <summary>
        /// 偶数个数
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> ouShuGeShu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in ouShuGpb.Controls)
            {
                if(ctl is CheckBox){
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (oneNums==null || oneNums.Count ==0 )
            {
                return heShuGeShu(allData);
            }

            foreach (string oneLineData in allData)
            {
                foreach (string oneNum in oneNums)
                {
                    //如果偶数中存在那么加入
                    bool isAdd = numCountNeeded(oneLineData, oneNum, ouShuArray);
                    if (isAdd)
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return heShuGeShu(resData);
        }

        /// <summary>
        /// 合数个数
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> heShuGeShu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in heShuGpb.Controls)
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
                return xiaoShuGeShu(allData);
            }

            foreach (string oneLineData in allData)
            {
                foreach (string oneNum in oneNums)
                {
                    //如果合数中存在那么加入
                    bool isAdd = numCountNeeded(oneLineData, oneNum, heShuArray);
                    if (isAdd)
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return xiaoShuGeShu(resData);
        }

        /// <summary>
        /// 小数个数
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> xiaoShuGeShu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in xiaoShuGpb.Controls)
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
                return lianHaoGeShu(allData);
            }

            foreach (string oneLineData in allData)
            {
                foreach (string oneNum in oneNums)
                {
                    //如果小数中存在那么加入
                    bool isAdd = numCountNeeded(oneLineData, oneNum, xiaoShuArray);
                    if (isAdd)
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return lianHaoGeShu(resData);
        }

        /// <summary>
        /// 连号个数
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> lianHaoGeShu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in lianHaoGpb.Controls)
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
                return niWeiGeShu(allData);
            }

            foreach (string oneLineData in allData)
            {
                foreach (string oneNum in oneNums)
                {
                    //如果存在连号那么加入
                    bool isAdd = numCountNeededForLianHao(oneLineData, oneNum);
                    if (isAdd)
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return niWeiGeShu(resData);
        }

        /// <summary>
        /// 逆位 01 02 03 04 05个数
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> niWeiGeShu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in niWeiGpb.Controls)
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
                return zhengWeiGeShu(allData);
            }

            foreach (string oneLineData in allData)
            {
                foreach (string oneNum in oneNums)
                {
                    bool isAdd = numCountNeeded(oneLineData, oneNum, niWeiArray);
                    if (isAdd)
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return zhengWeiGeShu(resData);
        }

        /// <summary>
        /// 正位 07 08 09 10 11个数
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> zhengWeiGeShu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in zhengWeiGpb.Controls)
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
                return lianHaoDuanShu(allData);
            }

            foreach (string oneLineData in allData)
            {
                foreach (string oneNum in oneNums)
                {
                    bool isAdd = numCountNeeded(oneLineData, oneNum, zhengWeiArray);
                    if (isAdd)
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return lianHaoDuanShu(resData);
        }

        /// <summary>
        /// 连号组数
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> lianHaoDuanShu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in duanShuGpb.Controls)
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
                return pingHengZhiShu(allData);
            }

            foreach (string oneLineData in allData)
            {
                string[] oneLineDatas = oneLineData.Split(new string[] { fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
                int no_1 = Convert.ToInt16(oneLineDatas[0]);
                int no_2 = Convert.ToInt16(oneLineDatas[1]);
                int no_3 = Convert.ToInt16(oneLineDatas[2]);
                int no_4 = Convert.ToInt16(oneLineDatas[3]);
                int no_5 = Convert.ToInt16(oneLineDatas[4]);
                int reason_1 = Math.Abs(no_1 - no_2);
                int reason_2 = Math.Abs(no_2 - no_3);
                int reason_3 = Math.Abs(no_3 - no_4);
                int reason_4 = Math.Abs(no_4 - no_5);

                if (oneNums.Contains("0"))
                {
                    if (reason_1 != 1 && reason_2 != 1 && reason_3 != 1 && reason_4 != 1)
                    {
                        resData.Add(oneLineData);
                    }
                }
                if (oneNums.Contains("1"))
                {
                    if (
                        (reason_1 == 1 && reason_4 != 1)
                        ||
                        (reason_2 == 1 && reason_3 == 1)
                        ||
                        (reason_4 == 1 && reason_1 != 1)
                        )
                    {
                        resData.Add(oneLineData);
                    }
                }
                if (oneNums.Contains("2"))
                {
                    if (
                        (reason_1 == 1 && reason_2 != 1 && reason_3 == 1)
                        ||
                        (reason_1 == 1 && reason_2 != 1 && reason_4 == 1)
                        ||
                        (reason_1 == 1 && reason_3 != 1 && reason_4 == 1)
                        ||
                        (reason_2 == 1 && reason_3 != 1 && reason_4 == 1)
                        )
                    {
                        resData.Add(oneLineData);
                    }
                }
            }

            return pingHengZhiShu(resData);
        }

        /// <summary>
        /// 平衡指数
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> pingHengZhiShu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> resDataForZheng = new List<string>();
            List<string> resDataForFu = new List<string>();
            List<string> resDataForDeng = new List<string>();
            List<string> conditions = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in pingHengGpb.Controls)
            {
                //+=-
                if ((ctl is CheckBox) && ctl.Name.IndexOf("pingHeng_") > -1)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        conditions.Add(ctl.Text);
                    }
                }

                //出0,1
                if ((ctl is CheckBox) && ctl.Name.IndexOf("pingHengChu") > -1)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (conditions == null || conditions.Count == 0 || oneNums == null || oneNums.Count == 0)
            {
                return lianHaoGuiJi(allData);
            }

            foreach (string oneLineData in allData)
            {
                //1为左边大于右边，-1为右边大于左边，0为两边相等
                int result = calcPingHengLeftOrRight(oneLineData);
                if (result == 1)
                {
                    resDataForZheng.Add(oneLineData);
                }
                else if (result == 0)
                {
                    resDataForDeng.Add(oneLineData);
                }
                else
                {
                    resDataForFu.Add(oneLineData);
                }
            }

            if (oneNums.Contains("1"))
            {
                if (conditions.Contains("+"))
                {
                    resData.AddRange(resDataForZheng);
                }
                if (conditions.Contains("="))
                {
                    resData.AddRange(resDataForDeng);
                }
                if (conditions.Contains("-"))
                {
                    resData.AddRange(resDataForFu);
                }
            }
            if (conditions.Count == 1)
            {
                if (oneNums.Contains("0"))
                {
                    if (conditions.Contains("+"))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti0(allData, resDataForZheng));
                    }
                    if (conditions.Contains("="))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti0(allData, resDataForDeng));
                    }
                    if (conditions.Contains("-"))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti0(allData, resDataForFu));
                    }
                }
            }
            else if (conditions.Count == 2)
            {
                if (oneNums.Contains("0"))
                {
                    if (conditions.Contains("+") && conditions.Contains("-"))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti1(allData, resDataForZheng, resDataForFu));
                    }
                    if (conditions.Contains("=") && conditions.Contains("-"))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti1(allData, resDataForDeng, resDataForFu));
                    }
                    if (conditions.Contains("+") && conditions.Contains("="))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti1(allData, resDataForZheng, resDataForDeng));
                    }
                }
            }
            return lianHaoGuiJi(resData);
        }

        /// <summary>
        /// 连号轨迹
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> lianHaoGuiJi(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> resDataForZheng = new List<string>();
            List<string> resDataForFu = new List<string>();
            List<string> resDataForDeng = new List<string>();
            List<string> conditions = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in lianHaoGuiJiGpb.Controls)
            {
                //+=-
                if ((ctl is CheckBox) && ctl.Name.IndexOf("lhgj_") > -1)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        conditions.Add(ctl.Text);
                    }
                }

                //出0,1
                if ((ctl is CheckBox) && ctl.Name.IndexOf("lhgjChu") > -1)
                {
                    if ((ctl as CheckBox).Checked == true)
                    {
                        oneNums.Add(ctl.Text);
                    }
                }
            }

            if (conditions == null || conditions.Count == 0 || oneNums == null || oneNums.Count == 0)
            {
                return pingHengGuiJi(allData);
            }

            foreach (string oneLineData in allData)
            {
                //1为左边大于右边，-1为右边大于左边，0为两边相等
                int result = calcLianHaoGuiJiLeftOrRight(oneLineData);
                if (result == 1)
                {
                    resDataForZheng.Add(oneLineData);
                }
                else if (result == 0)
                {
                    resDataForDeng.Add(oneLineData);
                }
                else
                {
                    resDataForFu.Add(oneLineData);
                }
            }

            if (oneNums.Contains("1"))
            {
                if (conditions.Contains("+"))
                {
                    resData.AddRange(resDataForZheng);
                }
                if (conditions.Contains("="))
                {
                    resData.AddRange(resDataForDeng);
                }
                if (conditions.Contains("-"))
                {
                    resData.AddRange(resDataForFu);
                }
            }
            if (conditions.Count == 1)
            {
                if (oneNums.Contains("0"))
                {
                    if (conditions.Contains("+"))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti0(allData, resDataForZheng));
                    }
                    if (conditions.Contains("="))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti0(allData, resDataForDeng));
                    }
                    if (conditions.Contains("-"))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti0(allData, resDataForFu));
                    }
                }
            }
            else if (conditions.Count == 2)
            {
                if (oneNums.Contains("0"))
                {
                    if (conditions.Contains("+") && conditions.Contains("-"))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti1(allData, resDataForZheng, resDataForFu));
                    }
                    if (conditions.Contains("=") && conditions.Contains("-"))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti1(allData, resDataForDeng, resDataForFu));
                    }
                    if (conditions.Contains("+") && conditions.Contains("="))
                    {
                        resData.AddRange(calcPingHengZhiShuForMulti1(allData, resDataForZheng, resDataForDeng));
                    }
                }
            }

            return pingHengGuiJi(resData);
        }

        /// <summary>
        /// 平衡轨迹
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> pingHengGuiJi(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in pingHengGuiJiGpb.Controls)
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
                return allData;
            }

            foreach (string oneLineData in allData)
            {
                //1为左边大于右边，-1为右边大于左边，0为两边相等
                int resultPH = calcPingHengLeftOrRight(oneLineData);
                int resultLHGJ = calcLianHaoGuiJiLeftOrRight(oneLineData);

                if (resultPH != resultLHGJ && oneNums.Contains("*"))
                {
                    resData.Add(oneLineData);
                }
                if (resultPH == resultLHGJ && resultPH == 0 && oneNums.Contains("="))
                {
                    resData.Add(oneLineData);
                }
                if (resultPH == resultLHGJ && resultPH == -1 && oneNums.Contains("-"))
                {
                    resData.Add(oneLineData);
                }
                if (resultPH == resultLHGJ && resultPH == 1 && oneNums.Contains("+"))
                {
                    resData.Add(oneLineData);
                }
            }
            return resData;
        }

        /// <summary>
        /// 号码散度
        /// </summary>
        /// <param name="allData"></param>
        /// <returns></returns>
        private List<string> haoMaSanDu(List<string> allData)
        {
            List<string> resData = new List<string>();
            List<string> oneNums = new List<string>();
            foreach (Control ctl in sanDuGpb.Controls)
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
                return allData;
            }

            foreach (string oneLineData in allData)
            {
                foreach (string oneNum in oneNums)
                {
                    bool isAdd = numCountNeededForSanDu(oneLineData, oneNum);
                    if (isAdd)
                    {
                        resData.Add(oneLineData);
                    }
                }
            }
            return resData;
        }

        /// <summary>
        /// 计算平衡指数个数
        /// </summary>
        /// <param name="oneLineData"></param>
        /// <returns>1为左边大于右边，-1为右边大于左边，0为两边相等</returns>
        private int calcPingHengLeftOrRight(string oneLineData) {
            string[] oneLineDatas = oneLineData.Split(new string[] { fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            int leftNums = 0;
            int rightNums = 0;

            while (i < oneLineDatas.Length)
            {
                //左边个数
                if (pingHengLeftArray.Contains(oneLineDatas[i]))
                {
                    leftNums++;
                }
                //右边个数
                if (pingHengRightArray.Contains(oneLineDatas[i]))
                {
                    rightNums++;
                }
                i++;
            }
            if (leftNums > rightNums)
            {
                return 1;
            }
            else if (leftNums < rightNums)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 计算连号轨迹个数
        /// </summary>
        /// <param name="oneLineData"></param>
        /// <returns>1为左边大于右边，-1为右边大于左边，0为两边相等</returns>
        private int calcLianHaoGuiJiLeftOrRight(string oneLineData)
        {
            int leftNums = 0;
            int rightNums = 0;

            foreach (string leftDatas in lianHaoLeftArray)
            {
                if (oneLineData.IndexOf(leftDatas)>-1)
                {
                    leftNums++;
                }
            }

            foreach (string rightDatas in lianHaoRightArray)
            {
                if (oneLineData.IndexOf(rightDatas) > -1)
                {
                    rightNums++;
                }
            }

            if (leftNums > rightNums)
            {
                return 1;
            }
            else if (leftNums < rightNums)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 用于计算勾选多个出0个的平衡指数
        /// </summary>
        private List<string> calcPingHengZhiShuForMulti0(List<string> allData, List<string> srcData)
        {
            List<string> tempList = new List<string>();
            tempList.AddRange(allData);
            foreach (string aData in allData)
            {
                foreach (string aSrcData in srcData)
                {
                    if (aData.Equals(aSrcData))
                    {
                        tempList.Remove(aSrcData);
                    }
                }
            }
            return tempList;
        }

        /// <summary>
        /// 用于计算勾选多个出个的平衡指数
        /// </summary>
        private List<string> calcPingHengZhiShuForMulti1(List<string> allData, List<string> srcData1, List<string> srcData2)
        {
            List<string> tempList = new List<string>();
            tempList.AddRange(allData);
            foreach (string aData in allData)
            {
                foreach (string aSrcData in srcData1)
                {
                    if (aData.Equals(aSrcData))
                    {
                        tempList.Remove(aSrcData);
                    }
                }
                foreach (string aSrcData in srcData2)
                {
                    if (aData.Equals(aSrcData))
                    {
                        tempList.Remove(aSrcData);
                    }
                }
            }
            return tempList;
        }

        /// <summary>
        /// 计算出个数
        /// </summary>
        /// <param name="oneLineData">每行数据</param>
        /// <param name="needNumCount">要出个数</param>
        /// <param name="needNumArray">筛哪组数据</param>
        /// <returns></returns>
        private bool numCountNeeded(string oneLineData,string needNumCount,string[] needNumArray) {
            int countNeedNum = 0;

            foreach(string needNum in needNumArray){
                if (oneLineData.IndexOf(needNum) > -1)
                {
                    countNeedNum++;
                }
            }

            if (countNeedNum == Convert.ToInt16(needNumCount))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计算连号出个数
        /// </summary>
        /// <param name="oneLineData">每行数据</param>
        /// <param name="needNumCount">要出个数</param>
        /// <param name="needNumArray">筛哪组数据</param>
        /// <returns></returns>
        private bool numCountNeededForLianHao(string oneLineData, string needNumCount)
        {
            int countNeedNum = 0;
            int i = 0;

            string[] oneLineDatas = oneLineData.Split(new string[] { fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
            while (i < oneLineDatas.Length - 1)
            {
                if (Convert.ToInt16(oneLineDatas[i + 1]) - Convert.ToInt16(oneLineDatas[i]) == 1)
                {
                    countNeedNum++;
                }
                i++;
            }

            if (countNeedNum == Convert.ToInt16(needNumCount))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计算号码散度出个数
        /// </summary>
        private bool numCountNeededForSanDu(string oneLineData, string needNumCount)
        {
            string[] oneLineDatas = oneLineData.Split(new string[] { fenGeFu }, StringSplitOptions.RemoveEmptyEntries);
            int no_1 = Convert.ToInt16(oneLineDatas[0]);
            int no_2 = Convert.ToInt16(oneLineDatas[1]);
            int no_3 = Convert.ToInt16(oneLineDatas[2]);
            int no_4 = Convert.ToInt16(oneLineDatas[3]);
            int no_5 = Convert.ToInt16(oneLineDatas[4]);
            //第一位和第二位的差或者第四位和第五位的差等于勾选的要出数字
            int reason_1 = Math.Abs(no_1 - no_2);
            int reason_2 = Math.Abs(no_2 - no_3);
            int reason_11 = Math.Abs(no_3 - no_4);
            int reason_22 = Math.Abs(no_4 - no_5);
            if (
                    (
                        reason_1 == Convert.ToInt16(needNumCount)
                        &&
                        reason_2 != Convert.ToInt16(needNumCount)
                        &&
                        reason_11 != Convert.ToInt16(needNumCount)
                        &&
                        reason_22 != Convert.ToInt16(needNumCount)
                    )
                    /*||
                    (
                        reason_1 != Convert.ToInt16(needNumCount)
                        &&
                        reason_2 == Convert.ToInt16(needNumCount)
                        &&
                        reason_11 != Convert.ToInt16(needNumCount)
                        &&
                        reason_22 != Convert.ToInt16(needNumCount)
                    )*/
                    /*||
                    (
                        reason_1 != Convert.ToInt16(needNumCount)
                        &&
                        reason_2 != Convert.ToInt16(needNumCount)
                        &&
                        reason_11 == Convert.ToInt16(needNumCount)
                        &&
                        reason_22 != Convert.ToInt16(needNumCount)
                    )*/
                    ||
                    (
                        reason_1 != Convert.ToInt16(needNumCount)
                        &&
                        reason_2 != Convert.ToInt16(needNumCount)
                        &&
                        reason_11 != Convert.ToInt16(needNumCount)
                        &&
                        reason_22 == Convert.ToInt16(needNumCount)
                    )
               )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 用于计算号码组的出0个
        /// </summary>
        /// <returns></returns>
        private List<string> calcHaoMaZuFor0(List<string> allData, List<string> srcNums)
        {
            List<string> resLi = new List<string>();
            int srcNumsCount = srcNums.Count;

            foreach (string oneLineData in allData)
            {
                bool noneThisNum = true;
                for (int i = 0; i < srcNumsCount; i++)
                {
                    string currStr = srcNums[i];
                    //如果含有要出的数，且没有要出的数之外所勾选的数
                    if (oneLineData.IndexOf(currStr) > -1)
                    {
                        noneThisNum = false;
                    }
                }
                if (noneThisNum)
                {
                    resLi.Add(oneLineData);//将分割前的正确数据插入结果集
                }
            }
            resLi.Sort();
            return resLi;
        }


        /// <summary>
        /// 用于计算号码组的出1个
        /// </summary>
        /// <returns></returns>
        private List<string> calcHaoMaZuFor1(List<string> allData, List<string> srcNums, int choiceCount)
        {
            List<string> resLi = new List<string>();
            int srcNumsCount = srcNums.Count;
            int srcNumsCountLessOne = srcNumsCount - 1;

            for (int i = 0; i < srcNumsCount; i++)
            {
                string currStr = srcNums[i];
                foreach (string oneLineData in allData)
                {
                    //如果含有要出的数，且没有要出的数之外所勾选的数
                    if (oneLineData.IndexOf(currStr) > -1)
                    {
                        List<string> tempSrcNums = new List<string>();
                        tempSrcNums.AddRange(srcNums);//用于将其他多余数字筛除
                        tempSrcNums.Remove(currStr);
                        if (noNeedNumClear(tempSrcNums, oneLineData))
                        {
                            resLi.Add(oneLineData);//将分割前的正确数据插入结果集
                        }
                    }
                }
            }
            resLi.Sort();
            return resLi;
        }

        /// <summary>
        /// 用于计算号码组的出2个
        /// </summary>
        /// <returns></returns>
        private List<string> calcHaoMaZuFor2(List<string> allData, List<string> srcNums,int choiceCount)
        {
            List<string> resLi = new List<string>();
            int srcNumsCount = srcNums.Count;
            int srcNumsCountLessOne = srcNumsCount - 1;

            for (int i = 0; i < srcNumsCount; i++)
            {
                string currStr = srcNums[i];
                int temp_i = i;
                
                //组成每一个“出2位数”
                while (temp_i < srcNumsCountLessOne)
                {
                    temp_i++;
                    string nextStr = srcNums[temp_i];

                    foreach (string oneLineData in allData)
                    {
                        //如果含有要出的数，且没有要出的数之外所勾选的数
                        if (oneLineData.IndexOf(currStr) > -1 && 
                            oneLineData.IndexOf(nextStr) > -1)
                        {
                            List<string> tempSrcNums = new List<string>();
                            tempSrcNums.AddRange(srcNums);//用于将其他多余数字筛除
                            tempSrcNums.Remove(currStr);
                            tempSrcNums.Remove(nextStr);
                            if (noNeedNumClear(tempSrcNums, oneLineData))
                            {
                                resLi.Add(oneLineData);//将分割前的正确数据插入结果集
                            }
                        }
                    }
                }
            }
            resLi.Sort();
            return resLi;
        }

        /// <summary>
        /// 用于计算号码组的出3个
        /// </summary>
        /// <returns></returns>
        private List<string> calcHaoMaZuFor3(List<string> allData, List<string> srcNums, int choiceCount)
        {
            List<string> resLi = new List<string>();
            int srcNumsCount = srcNums.Count;
            int srcNumsCountLessOne = srcNumsCount - 1;

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

                        foreach (string oneLineData in allData)
                        {
                            //如果含有要出的数，且没有要出的数之外所勾选的数
                            if (oneLineData.IndexOf(currStr) > -1 &&
                                oneLineData.IndexOf(nextStr) > -1 &&
                                oneLineData.IndexOf(next2Str) > -1)
                            {
                                List<string> tempSrcNums = new List<string>();
                                tempSrcNums.AddRange(srcNums);//用于将其他多余数字筛除
                                tempSrcNums.Remove(currStr);
                                tempSrcNums.Remove(nextStr);
                                tempSrcNums.Remove(next2Str);
                                if (noNeedNumClear(tempSrcNums, oneLineData))
                                {
                                    resLi.Add(oneLineData);//将分割前的正确数据插入结果集
                                }
                            }
                        }
                    }
                }
            }
            resLi.Sort();
            return resLi;
        }

        /// <summary>
        /// 用于计算号码组的出4个
        /// </summary>
        /// <returns></returns>
        private List<string> calcHaoMaZuFor4(List<string> allData, List<string> srcNums, int choiceCount)
        {
            List<string> resLi = new List<string>();
            int srcNumsCount = srcNums.Count;
            int srcNumsCountLessOne = srcNumsCount - 1;

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

                            foreach (string oneLineData in allData)
                            {
                                //如果含有要出的数，且没有要出的数之外所勾选的数
                                if (oneLineData.IndexOf(currStr) > -1 &&
                                    oneLineData.IndexOf(nextStr) > -1 &&
                                    oneLineData.IndexOf(next2Str) > -1 &&
                                    oneLineData.IndexOf(next3Str) > -1)
                                {
                                    List<string> tempSrcNums = new List<string>();
                                    tempSrcNums.AddRange(srcNums);//用于将其他多余数字筛除
                                    tempSrcNums.Remove(currStr);
                                    tempSrcNums.Remove(nextStr);
                                    tempSrcNums.Remove(next2Str);
                                    tempSrcNums.Remove(next3Str);
                                    if (noNeedNumClear(tempSrcNums, oneLineData))
                                    {
                                        resLi.Add(oneLineData);//将分割前的正确数据插入结果集
                                    }
                                }
                            }
                        }
                    }
                }
            }
            resLi.Sort();
            return resLi;
        }

        /// <summary>
        /// 用于计算号码组的出5个
        /// </summary>
        /// <returns></returns>
        private List<string> calcHaoMaZuFor5(List<string> allData, List<string> srcNums, int choiceCount)
        {
            List<string> resLi = new List<string>();
            int srcNumsCount = srcNums.Count;
            int srcNumsCountLessOne = srcNumsCount - 1;

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

                                foreach (string oneLineData in allData)
                                {
                                    //如果含有要出的数，且没有要出的数之外所勾选的数
                                    if (oneLineData.IndexOf(currStr) > -1 &&
                                        oneLineData.IndexOf(nextStr) > -1 &&
                                        oneLineData.IndexOf(next2Str) > -1 &&
                                        oneLineData.IndexOf(next3Str) > -1 &&
                                        oneLineData.IndexOf(next4Str) > -1)
                                    {
                                        List<string> tempSrcNums = new List<string>();
                                        tempSrcNums.AddRange(srcNums);//用于将其他多余数字筛除
                                        tempSrcNums.Remove(currStr);
                                        tempSrcNums.Remove(nextStr);
                                        tempSrcNums.Remove(next2Str);
                                        tempSrcNums.Remove(next3Str);
                                        tempSrcNums.Remove(next4Str);
                                        if (noNeedNumClear(tempSrcNums, oneLineData))
                                        {
                                            resLi.Add(oneLineData);//将分割前的正确数据插入结果集
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            resLi.Sort();
            return resLi;
        }

        /// <summary>
        /// 如果当前的数不在这行中，但是又勾选了，那么摘出去
        /// </summary>
        /// <returns></returns>
        private bool noNeedNumClear(List<string> tempSrcNums,string newOneLineData)
        {
            List<string> newTemp = new List<string>();
            newTemp.AddRange(tempSrcNums);
            foreach (string tempSrcNum in tempSrcNums)
            {
                int tempSrcNumInt = Convert.ToInt16(tempSrcNum);
                int tempLastTwoNum = Convert.ToInt16(newOneLineData.Substring(newOneLineData.Length - 2));
                if (tempSrcNumInt <= tempLastTwoNum)
                {
                    if (newOneLineData.IndexOf(tempSrcNum) > -1)
                    {
                        return false;
                    }
                    else {
                        newTemp.Remove(tempSrcNum);
                        return noNeedNumClear(newTemp, newOneLineData);
                    }

                }
            }
            return true;
        }

        #region 控件事件
        /// <summary>
        /// 分隔符Combobox改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fenGeCombx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string txt = this.fenGeCombx.Text;
            if(txt.Equals("空格")){
                fenGeFu = " ";
            }
            else if (txt.Equals("中文逗号，"))
            {
                fenGeFu = "，";
            }
            else if (txt.Equals("英文逗号,"))
            {
                fenGeFu = ",";
            }
            else if (txt.Equals("中文分号；"))
            {
                fenGeFu = "；";
            }
            else if (txt.Equals("英文分号;"))
            {
                fenGeFu = ";";
            }
            getMainData();
        }

        //号码组区域Combobox监控器1
        private void timer1_Tick(object sender, EventArgs e)
        {
            visibleZuCbx1(this.oneZuGpb, "oneNum", "oneZu");
            visibleZuCbx2(this.twoZuGpb, "twoNum", "twoZu");
            visibleZuCbx3(this.threeZuGpb, "threeNum", "threeZu");
            visibleZuCbx4(this.fourZuGpb, "fourNum", "fourZu");
        }

        private void visibleZuCbx1(Control Gpb, string oneNumName, string oneZuName)
        {
            int countOneNum = 0;

            if (countOneHaoZuCbxOld == 0)
            {
                foreach (Control ctl in Gpb.Controls)
                {
                    if (ctl is CheckBox && ctl.Name.Contains(oneZuName))
                    {
                        ctl.Enabled = false;
                    }
                }
            }

            //判断有勾了几个数
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && (ctl as CheckBox).Checked && ctl.Name.Contains(oneNumName))
                {
                    countOneNum++;
                }
            }

            //把所有Enable=false的勾去掉
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && ctl.Name.Contains(oneZuName) && ctl.Enabled == false)
                {
                    (ctl as CheckBox).Checked = false;
                }
            }

            if (countOneNum == countOneHaoZuCbxOld)
            {
                return;
            }
            countOneHaoZuCbxOld = countOneNum;
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && ctl.Name.Contains(oneZuName))
                {
                    ctl.Enabled = false;
                    for (int i = 0; i <= countOneNum; i++)
                    {
                        if (ctl.Name.Contains(i.ToString()))
                        {
                            ctl.Enabled = true;
                        }
                    }
                }

            }
        }

        private void visibleZuCbx2(Control Gpb, string oneNumName, string oneZuName)
        {
            int countOneNum = 0;

            if (countTwoHaoZuCbxOld == 0)
            {
                foreach (Control ctl in Gpb.Controls)
                {
                    if (ctl is CheckBox && ctl.Name.Contains(oneZuName))
                    {
                        ctl.Enabled = false;
                    }
                }
            }

            //判断有勾了几个数
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && (ctl as CheckBox).Checked && ctl.Name.Contains(oneNumName))
                {
                    countOneNum++;
                }
            }

            //把所有Enable=false的勾去掉
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && ctl.Name.Contains(oneZuName) && ctl.Enabled == false)
                {
                    (ctl as CheckBox).Checked = false;
                }
            }

            if (countOneNum == countTwoHaoZuCbxOld)
            {
                return;
            }
            countTwoHaoZuCbxOld = countOneNum;
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && ctl.Name.Contains(oneZuName))
                {
                    ctl.Enabled = false;
                    for (int i = 0; i <= countOneNum; i++)
                    {
                        if (ctl.Name.Contains(i.ToString()))
                        {
                            ctl.Enabled = true;
                        }
                    }
                }

            }
        }

        private void visibleZuCbx3(Control Gpb, string oneNumName, string oneZuName)
        {
            int countOneNum = 0;

            if (countThreeHaoZuCbxOld == 0)
            {
                foreach (Control ctl in Gpb.Controls)
                {
                    if (ctl is CheckBox && ctl.Name.Contains(oneZuName))
                    {
                        ctl.Enabled = false;
                    }
                }
            }

            //判断有勾了几个数
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && (ctl as CheckBox).Checked && ctl.Name.Contains(oneNumName))
                {
                    countOneNum++;
                }
            }

            //把所有Enable=false的勾去掉
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && ctl.Name.Contains(oneZuName) && ctl.Enabled == false)
                {
                    (ctl as CheckBox).Checked = false;
                }
            }

            if (countOneNum == countThreeHaoZuCbxOld)
            {
                return;
            }
            countThreeHaoZuCbxOld = countOneNum;
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && ctl.Name.Contains(oneZuName))
                {
                    ctl.Enabled = false;
                    for (int i = 0; i <= countOneNum; i++)
                    {
                        if (ctl.Name.Contains(i.ToString()))
                        {
                            ctl.Enabled = true;
                        }
                    }
                }

            }
        }

        private void visibleZuCbx4(Control Gpb, string oneNumName, string oneZuName)
        {
            int countOneNum = 0;

            if (countFourHaoZuCbxOld == 0)
            {
                foreach (Control ctl in Gpb.Controls)
                {
                    if (ctl is CheckBox && ctl.Name.Contains(oneZuName))
                    {
                        ctl.Enabled = false;
                    }
                }
            }

            //判断有勾了几个数
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && (ctl as CheckBox).Checked && ctl.Name.Contains(oneNumName))
                {
                    countOneNum++;
                }
            }

            //把所有Enable=false的勾去掉
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && ctl.Name.Contains(oneZuName) && ctl.Enabled == false)
                {
                    (ctl as CheckBox).Checked = false;
                }
            }

            if (countOneNum == countFourHaoZuCbxOld)
            {
                return;
            }
            countFourHaoZuCbxOld = countOneNum;
            foreach (Control ctl in Gpb.Controls)
            {
                if (ctl is CheckBox && ctl.Name.Contains(oneZuName))
                {
                    ctl.Enabled = false;
                    for (int i = 0; i <= countOneNum; i++)
                    {
                        if (ctl.Name.Contains(i.ToString()))
                        {
                            ctl.Enabled = true;
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

        private void oneZuClearBtn_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.oneZuGpb);
        }

        private void twoZuClearBtn_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.twoZuGpb);
        }

        private void threeZuClearBtn_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.threeZuGpb);
        }

        private void fourZuClearBtn_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.fourZuGpb);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.ouShuGpb);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.heShuGpb);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.xiaoShuGpb);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.lianHaoGpb);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.duanShuGpb);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.sanDuGpb);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.niWeiGpb);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clearSingleCheckbox(this.zhengWeiGpb);
        }

        /// <summary>
        /// 清空单个框体里面的单选框勾选
        /// </summary>
        /// <param name="ctrls"></param>
        private void clearSingleCheckbox(Control ctrls)
        {
            foreach (Control cbx in ctrls.Controls)
            {
                if(cbx is CheckBox){
                    (cbx as CheckBox).Checked = false;
                }
            }
        }
        #endregion
    }
}

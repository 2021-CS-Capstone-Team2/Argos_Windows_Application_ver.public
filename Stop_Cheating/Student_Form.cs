using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Threading;
using System.IO.Ports;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Net;
using System.IO;
using Microsoft.VisualBasic;

namespace Stop_Cheating
{
    /*
     동작 순서 : exe실행 -> button 누르면 zoom 감지 -> 감지되면 현 프로세스, usb, 디바이스 상황 스캔 후 db 전송 (프로세스는 특이사항없으면 전송 x)
     ->  프로세스 , usb, 디바이스, 마우스 키보드 이벤트 모니터링 시작
     -> 모두 이벤트 발생 시 마다 전송
     -> Stop 누르면 모든 동작 중단
 
         */


    //모두 이벤트 기반 수집
    public partial class Student_Form : MetroFramework.Forms.MetroForm
    {
        //mysql connection 주소
        public string strConn;
        //member id, email, sub, 학번, 이름
        protected string std_id = "";
        protected int memberid = 37;
        protected string useremail;
        protected string memberemail;
        protected string examcode = "RjA5My0xMjAyMS0xTUlEVEVSTQ==";
        protected Boolean membersuccess = true;
        protected Boolean Loginsuccess = false;
        protected Boolean Detectsuccess = false;

        protected Boolean ps_status = true;
        protected Boolean ms_status = true;
        protected Boolean kb_status = true;
        protected Boolean au_status = true;
        protected Boolean ey_status = true;

        //마우스 키보드 이벤트 
        protected Task mktask; //독립 쓰레드 (EventWatcher 기능)
        CancellationTokenSource cts; //쓰레드 종료 토큰
        CancellationToken token;
        [DllImport("MouseandKeyboardEvent.dll", CallingConvention = CallingConvention.Cdecl)] //c++ 로 마우스, 키보드 이벤트 수집 dll 참조
        static extern char Detect_events();
        //char 데이터 string으로 매핑
        protected string[,] mkmapping;

        protected string[,] hkmapping;
        // stop이나 종료 시 반복문 탈출
        protected Boolean stopmk = false;

        //실행 중인 앱 목록
        protected List<string> Program_list = new List<string>();

        //백그라운드 프로세스 목록
        protected List<string> Process_list = new List<string>();

        //자기자신 프로세스
        protected Process thisprogram;


        //프로세스 이벤트 탐지용 변수
        protected ManagementEventWatcher ProcessWatcher;    

        //USB caption
        protected string strUSBDriveName;
        //USB name
        protected string strUSBDriveLetter;
        protected int iCount = 0;

        //Device 이벤트 탐지용 변수
        protected ManagementEventWatcher DeviceWatcher;

        //파이썬 프로세스 병합을 위한 함수
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr Handle, int x, int y, int w, int h, bool repaint);

        static readonly int GWL_STYLE = -16;
        static readonly int WS_VISIBLE = 0x10000000;

        //비디오 오디오 데이터 폴더 이벤트 탐지용 변수
        protected FileSystemWatcher FileWatcher;

        public Student_Form()
        {

            //마우스 키보드 이벤트 매핑
            mkmapping = new string[,] {
                  {"left click","Q"},
                  {"right click", "W"},
                  {"center click", "E"},
                  {"x1 click", "R"},
                  {"x2 click", "T"},
                  {"backspace", "Y"},
                  {"enter", "U"},
                  {"shift", "I"},
                  {"ctrl", "O"},
                  {"alt", "P"},
                  {"tab", "{"},
                  {"clear", "}"},
                  {"pause", "A"},
                  {"capslock", "S"},
                  {"esc", "D"},
                  {"space bar", "F"},
                  {"page up", "G"},
                  {"page down", "H"},
                  {"end", "J"},
                  {"home", "K"},
                  {"left", "L"},
                  {"up", ":"},
                  {"right", "Z"},
                  {"down", "X"},
                  {"select", "C"},
                  {"print", "V"},
                  {"execute", "B"},
                  {"print screen", "N"},
                  {"insert", "M"},
                  {"delete", "<"},
                  {"window", ">"},
                  {"app", "?"},
                  {"F1", "~"},
                  {"F2", "!"},
                  {"F3", "@"},
                  {"F4", "#"},
                  {"F5", "$"},
                  {"F6", "%"},
                  {"F7", "^"},
                  {"F8", "&"},
                  {"F9", "*"},
                  {"F10", "("},
                  {"F11", ")"},
                  {"F12", "_"},
                  {"num lock", "+"}
               };
            //hotkey 매핑
            hkmapping = new string[,] {
      // {message, status, 리턴된 값}
      {"ctrl, alt, tab", "WAR_HOTKEY", "0"},
      {"ctrl, window, D", "DAN_HOTKEY", "1"},
      {"ctrl, window, left", "DAN_HOTKEY", "2"},
      {"ctrl, window, right", "DAN_HOTKEY", "3"},
      {"ctrl, tab", "WAR_HOTKEY", "4"},
      {"ctrl, page down", "WAR_HOTKEY", "5"},
      {"ctrl, page up", "WAR_HOTKEY", "6"},
      {"alt, tab", "WAR_HOTKEY", "7"},
      {"alt, ctrl, tab", "WAR_HOTKEY", "8"},
      {"alt, ESC", "WAR_HOTKEY", "9"},
      {"window, tab", "WAR_HOTKEY", "10"},
      {"window, ctrl, d", "DAN_HOTKEY", "11"},
      {"window, ctrl, right", "DAN_HOTKEY", "12"},
      {"window, ctrl, left", "DAN_HOTKEY", "13"},
      {"window, arrow", "WAR_HOTKEY", "14"},
      {"window, home", "WAR_HOTKEY", "15"},
      {"ctrl, shift, tab", "WAR_HOTKEY", "16"},
      {"ctrl, number", "WAR_HOTKEY", "17"},
      {"ctrl, x", "WAR_HOTKEY", "18"},
      {"ctrl, c", "WAR_HOTKEY", "19"},
      {"ctrl, v", "WAR_HOTKEY", "20"},
      {"ctrl, a", "WAR_HOTKEY", "21"},
      {"ctrl, z", "WAR_HOTKEY", "22"},
      {"ctrl, ESC", "DAN_HOTKEY", "23"},
      {"ctrl, shift, ESC", "DAN_HOTKEY", "24"},
      {"window, shift, left", "WAR_HOTKEY", "25"},
      {"window, shift, right", "WAR_HOTKEY", "26"},
      {"window, shift, s", "DAN_HOTKEY", "27"},
      {"window, g", "DAN_HOTKEY", "28"},
      {"window, e", "DAN_HOTKEY", "29"},
      {"window, v", "DAN_HOTKEY", "30"},
      {"shift, ctrl, ESC", "DAN_HOTKEY", "31"},
      {"shift, ctrl, tab", "WAR_HOTKEY", "32"},
      {"shift, window, right", "WAR_HOTKEY", "33"},
      {"shift, window, left", "WAR_HOTKEY", "34"},
      {"shift, window, s", "DAN_HOTKEY", "35"}
   };

            InitializeComponent();

            Tray_icon.ContextMenuStrip = Tray_menu;

            metroTabControl1.SelectedTab = LoginPage;

            //크로스 스레드 허용 -> 별도의 이벤트 쓰레드가 ui에 정보를 전달하기 위해 필요함 https://kdsoft-zeros.tistory.com/22 
            CheckForIllegalCrossThreadCalls = false;

            OAuthlogin oauth = new OAuthlogin();
            oauth.Login_start(LoginPage, data_label); //크롬 컨트롤과 깨짐 충돌 방지
            
            
            //자기 자신 프로세스 정보 수집
            thisprogram = Process.GetCurrentProcess(); 
        }

        private void data_label_TextChanged(object sender, EventArgs e)
        {
            std_id = Microsoft.VisualBasic.Interaction.InputBox("학번을 입력하시오", "Input Student id", "");

            string[] oauthoutput = data_label.Text.Split(' ');
            sqlInsert("member", oauthoutput[2], oauthoutput[0], oauthoutput[1]);
            data_label.Visible = false;
            if (membersuccess)
                sqlInsert("member_auth", "", "", "");
        }

        //Zoom 인식 시 동작 시작
        private void Detect_start_Click(object sender, EventArgs e)
        {
            if (!Loginsuccess)
            {
                MessageBox.Show("로그인이 완료되지 않았습니다. ", "Login is not completed");
            }
            else
            {
                if (Detectsuccess)
                    return;

                examcode = Microsoft.VisualBasic.Interaction.InputBox("시험 코드를 입력하시오", "Input Exam Code", "");

                if (IsZoomExcuting())
                {

                    sqlselect_exam();

                    if (Canaccess_exam_log())
                        sqlInsert("exam_log", "PROCEED", "", "");
                    else
                    {
                        MessageBox.Show("등록되지 않은 사용자/시험코드 입니다.", "Access denied");
                        return;
                    }

                    if(au_status || ey_status)
                    {
                        WaitForFile();
                        if (au_status)
                            AudioTracker();
                        if (ey_status)
                            VideoTracker();
                    }
                    Wait_second_label.Text = "Please wait about 10 seconds..";


                    whitelist_Inserting("FirstExecution");
                    FirstMonitor_Device();

                    if (!DBconnection_label.Text.Equals("False"))
                    {
                        DBconnection_label.Text = "True";
                        DBconnection_label.ForeColor = Color.Green;
                    }

                    Userinfo_label.Text = Environment.UserName;
                    IP_label.Text = GetExternalIPAddress();

                    if (DBconnection_label.Text.Equals("True"))
                    {
                        if (ps_status)
                            WaitForProcess();
                        WaitForDevice();

                        MousekeyboardEvent_task();
                    }

                    
                    this.Close();

                    Detectsuccess = true;

                    Detecting_mode_label.ForeColor = Color.Gray;
                    Restart_label.ForeColor = Color.Gray;
                    Wait_second_label.Text = "Activated";

                }
                else
                {
                    MessageBox.Show("Zoom이 실행되지 않고 있습니다.", "Zoom is not detected");
                }
            }

          

        }

        private Boolean Canaccess_exam_log()
        {
            try
            {
                using (MySqlConnection conn2 = new MySqlConnection(strConn))
                {
                    conn2.Open();
                    string command = "select count(c.id) from exam e,class_member c where e.code = '"+ examcode +"'"
                        + " and e.class_code = c.class_code " + "and e.semester = e.semester " + "and c.member_email = '"+ memberemail +"'; ";

                    Console.WriteLine(command);
                    MySqlCommand cmd2 = new MySqlCommand(command, conn2);
                    int returnnum = Convert.ToInt32(cmd2.ExecuteScalar());

                    if (returnnum == 0)
                        return false;
                    else
                        return true;

                }
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.ToString());
                if (MessageBox.Show("인터넷 연결에 실패했습니다. 다시 시작하십시오", "Login", MessageBoxButtons.OK).Equals(DialogResult.OK))
                {
                    exit_shutdown();
                }
            }
            return false;
        }

        private void Submission_button_Click(object sender, EventArgs e)
        {
            if(!Detectsuccess)
                MessageBox.Show("Detecting Mode가 활성화되지 않았습니다. ", "Detecting mode is not on");
            else
            {
                if (MessageBox.Show("제출하시겠습니까?", "Login", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
                {
                    sqlInsert("exam_log", "FINISH", "", "");

                    exit_shutdown();
                }
            }
            
        }

        private void exit_shutdown()
        {
            Process[] allProc = Process.GetProcesses();

            foreach (Process p in allProc)
            {
                if (p.ProcessName.Contains("speech_recog"))
                {
                    p.Kill();
                }

                if (p.ProcessName.Contains("eyetracking"))
                {
                    p.Kill();
                }

            }

            if (System.IO.Directory.Exists("C:\\ArgosAjou"))

            {

                string[] files = System.IO.Directory.GetFiles("C:\\ArgosAjou");

                foreach (string s in files)

                {

                    string fileName = System.IO.Path.GetFileName(s);

                    string deletefile = "C:\\ArgosAjou\\" + fileName;

                    try
                    {
                        if (fileName.Contains(".txt"))
                            System.IO.File.Delete(deletefile);
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine("txt 파일 2개쯤 남았음");
                    }

                    

                }

            }

            Application.Exit();
        }

        public static string GetExternalIPAddress()
        {
            string externalip = new WebClient().DownloadString("http://ipinfo.io/ip").Trim(); //http://icanhazip.com

            if (String.IsNullOrWhiteSpace(externalip))
            {
                externalip = GetExternalIPAddress();//null경우 Get Internal IP를 가져오게 한다.
            }

            return externalip;
        }

        public void sqlselect_exam()
        {
            try
            {
                using (MySqlConnection conn2 = new MySqlConnection(strConn))
                {
                    conn2.Open();
                    string command = "select id from exam where code=" + "'" + examcode + "'";
                    Console.WriteLine(command);
                    MySqlCommand cmd2 = new MySqlCommand(command, conn2);
                    int returnnum = Convert.ToInt32(cmd2.ExecuteScalar());
                    
                    command = "select * from exam_event_setting where exam_id=" + returnnum;
                    cmd2 = new MySqlCommand(command, conn2);
                    MySqlDataReader table = cmd2.ExecuteReader();
                    while (table.Read())
                    {
                        if (table["process_event"].ToString().Equals("N"))
                            ps_status = false;

                        if (table["keyboard_event"].ToString().Equals("N"))
                            kb_status = false;

                        if (table["mouse_event"].ToString().Equals("N"))
                            ms_status = false;

                        if (table["audio_event"].ToString().Equals("N"))
                            au_status = false;

                        if (table["eye_tracking_event"].ToString().Equals("N"))
                            ey_status = false;

                    }
                    table.Close();

                }
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.ToString());
                if (MessageBox.Show("인터넷 연결에 실패했습니다. 다시 시작하십시오", "Login", MessageBoxButtons.OK).Equals(DialogResult.OK))
                {
                    exit_shutdown();
                    
                }
            }
        }

        //DB에 데이터 삽입
        public void sqlInsert(string table, string first, string second, string third) //연동을 한 후에 insert만 하는걸로...
        {

            try
            {
                using (MySqlConnection conn = new MySqlConnection(strConn))
                {
                    conn.Open();
                    //현재 시각 설정.
                    DateTime datetime = DateTime.Now;


                    string command="";

                    if (table.Equals("member")) //password가 존재하면 실행되지 않음 type = STUDENT, dept_code = cyber_security , status = n
                    {
                        string[] emails = first.Split('+');
                        useremail = emails[0];
                        command = "insert into member(create_member_id, create_date, email, password, name, number, type, dept_code, status, hosted_domain)" +
                            " values(" + "'" + "admin" + "', " + "\"" + datetime.ToString("yyyy-MM-dd H:mm:ss") + "\"," + "'" + emails[0] + "'," + "'" + second + "'," +
                            "'" + third + "'," + "'" + std_id + "'," + "'" + "STUDENT" + "'," + "'" + "CYBER_SECURITY" + "'," + "'" + "N" + "','" + emails[1]  + "')";
                    }
                    else if (table.Equals("member_auth"))  //Authcode = student
                    {
                        command = "insert into member_auth(create_member_id, create_date, auth_code, member_id)" +
                            " values(" + "'" + "admin" + "', " + "\"" + datetime.ToString("yyyy-MM-dd H:mm:ss") + "\"," + "'" + "STUDENT" + "'," + memberid.ToString() + ")";
                    }
                    else if (table.Equals("exam_log"))
                    {
                        command = "insert into exam_log(create_member_id, create_date, member_email, exam_code, member_ip, status)" +
                            " values(" + "'" + memberid.ToString() + "', " + "\"" + datetime.ToString("yyyy-MM-dd H:mm:ss") + "\"," +  "'" + useremail + "'," + "'" + examcode + "'," +
                            "'" + GetExternalIPAddress() + "','" + first + "')";
                    }
                    else if (table.Equals("process_event"))
                    {
                        //process_listbox.Items.RemoveAt(0);
                        command = "insert into process_event(create_member_id, create_date, exam_code, event_process, detected_process, main_title)" +
                            " values(" + "'" + memberid.ToString() + "', " + "\"" + datetime.ToString("yyyy-MM-dd H:mm:ss") + "\"," +
                            "'" + examcode + "'," +
                             "'" + first + "'," + "'" + second + "'," + "'" + third + "')";
                        process_listbox.Items.Add(first + " / " + second + " / " + third);
                    }
                    else if(table.Equals("device_event"))
                    {
                        //device_listbox.Items.RemoveAt(0);
                        command = "insert into device_event(create_member_id, create_date, exam_code, type, status, message)" +
                            " values(" + "'" + memberid.ToString() + "', " + "\"" + datetime.ToString("yyyy-MM-dd H:mm:ss") + "\"," +
                            "'" + examcode + "'," +
                             "'" + first + "'," + "'" + second + "'," + "'" + third + "')";
                        device_listbox.Items.Add(first + "/" + second + "/" + third);
                    }

                    MySqlCommand cmd = new MySqlCommand(command, conn);      

                    if (table.Equals("member")) //member id 도출
                    {
                        cmd.ExecuteNonQuery();

                        try
                        {
                            using (MySqlConnection conn2 = new MySqlConnection(strConn))
                            {
                                conn2.Open();
                                command = "select max(id) from member";
                                MySqlCommand cmd2 = new MySqlCommand(command, conn2);
                                memberid = Convert.ToInt32(cmd2.ExecuteScalar());
                                Console.WriteLine(memberid);
                                conn2.Close();
                            }
                        }
                        catch(Exception e2){
                            Console.WriteLine(e2.ToString());
                            if (MessageBox.Show("인터넷 연결에 실패했습니다. 다시 시작하십시오", "Login", MessageBoxButtons.OK).Equals(DialogResult.OK))
                            {
                                exit_shutdown();
                            }
                        }       
                    }
                    else if (table.Equals("member_auth")) //최종 회원가입 성공
                    {
                        cmd.ExecuteNonQuery();
                        if(MessageBox.Show("회원가입에 성공했습니다.", "Login", MessageBoxButtons.OK).Equals(DialogResult.OK)) {
                            Loginsuccess = true;
                            metroTabControl1.SelectedTab = Startingmode;
                            clock_activate();
                        }

                     
                    }
                    else
                        cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch(Exception ee)
            { 
                if (table.Equals("member"))
                {
                    membersuccess = false;
                    if (ee.ToString().Contains("Duplicate")) //회원가입이 되어있으므로 로그인 or 매칭 오류로 로그인 실패
                    {
                        Console.WriteLine(ee.ToString());
                        string[] errors = ee.ToString().Split(' ');
                        for(int i = 0; i < errors.Length; i++)
                        {
                            if (errors[i].Equals("entry"))
                            {
                                memberemail = errors[i + 1].Replace("'", "").Replace("'", "");
                                Console.WriteLine(memberemail);
                                continue;
                            }
                            if (errors[i].Equals("key"))
                            {
                                try
                                {
                                    using (MySqlConnection conn2 = new MySqlConnection(strConn))
                                    {
                                        conn2.Open();

                                        if (errors[i + 1].Contains("'email'"))
                                        {
                                            string command = "select number from member where email=" + "'" + memberemail + "'";
                                            Console.WriteLine(command);
                                            MySqlCommand cmd2 = new MySqlCommand(command, conn2);
                                            string returnnum = (string)cmd2.ExecuteScalar();

                                            command = "select id from member where email=" + "'" + memberemail + "'";
                                            cmd2 = new MySqlCommand(command, conn2);
                                            memberid = Convert.ToInt32(cmd2.ExecuteScalar());

                                            if (std_id.Equals(returnnum))
                                            { //학번 일치로 로그인 성공
                                               
                                                if (MessageBox.Show("로그인에 성공했습니다.", "Login", MessageBoxButtons.OK).Equals(DialogResult.OK))
                                                {
                                                    Loginsuccess = true;
                                                    metroTabControl1.SelectedTab = Startingmode;
                                                    clock_activate();
                                                    
                                                }
                                            }
                                            else //학번 불일치로 로그인 실패 
                                            {
                                                MessageBox.Show("학번 불일치로 로그인에 실패했습니다. 다시 시도하십시오.", "Login");
                                               
                                            }
                                        }
                                        else if (errors[i + 1].Contains("'number'"))
                                        {
                                            string command = "select number from member where number=" + "'" + std_id + "'";
                                            Console.WriteLine(command);
                                            MySqlCommand cmd2 = new MySqlCommand(command, conn2);
                                            string returnnum = (string)cmd2.ExecuteScalar();

                                            command = "select id from member where email=" + "'" + memberemail + "'";
                                            cmd2 = new MySqlCommand(command, conn2);
                                            memberid = Convert.ToInt32(cmd2.ExecuteScalar());

                                            if (std_id.Equals(returnnum))
                                            { //학번 일치로 로그인 성공
                                                if (MessageBox.Show("로그인에 성공했습니다.", "Login", MessageBoxButtons.OK).Equals(DialogResult.OK))
                                                {
                                                    Loginsuccess = true;
                                                    metroTabControl1.SelectedTab = Startingmode;
                                                    clock_activate();
                                                }
                                            }
                                            else //학번 불일치로 로그인 실패 
                                            {
                                                MessageBox.Show("이메일 불일치로 로그인에 실패했습니다. 다시 시도하십시오.", "Login");
                                                
                                            }
                                        }
                                        conn2.Close();
                                    }
                                }
                                catch (Exception e2)
                                {
                                    Console.WriteLine(ee.ToString());
                                    if (MessageBox.Show("인터넷 연결에 실패했습니다. 다시 시작하십시오", "Login", MessageBoxButtons.OK).Equals(DialogResult.OK))
                                    {
                                        exit_shutdown();
                                    }
                                }


                                break;
                            }
                        }
                       
                    }
                    
                }
                else
                {
                    Console.WriteLine(ee.ToString());
                    if (MessageBox.Show("인터넷 연결에 실패했습니다. 다시 시작하십시오", "Login", MessageBoxButtons.OK).Equals(DialogResult.OK))
                    {
                        exit_shutdown();
                    }
                }
                
            }
           
        }

        private void clock_activate()
        {
            Timer1.Interval = 1000;
            Timer1.Start();
            Timer1.Tick += Timer1_Tick;
        }

        private void MousekeyboardEvent()
        {
            Hotkey hk = new Hotkey();
            int hknum = 999;

            while (true)
            {
                if (stopmk)
                {
                    stopmk = false;
                    break;
                }

                char temp = Detect_events();
                hknum = hk.Detect_Hotkey(temp);


                Boolean isexist = false;
                int mappingsize = mkmapping.Length / 2;
                for (int i = 0; i < mappingsize; i++)
                {
                    if (temp.ToString().Equals(mkmapping[i,1]))
                    {
                        if (mkmapping[i, 0].Contains("click"))
                        {//mouse
                            if (ms_status)
                                sqlInsert("device_event", "MOUSE", "MOUSE", mkmapping[i, 0]);
                        }
                        else
                        {
                            if (kb_status) {
                                

                                if (hknum != 999)
                                {
                                    for (int j = 0; j < hkmapping.Length; j++)
                                    {
                                        if (hkmapping[j, 2].Equals(hknum.ToString()))
                                        {
                                            sqlInsert("device_event", "KEYBOARD", hkmapping[j,1], hkmapping[j, 0]);
                                            break;
                                        }
                                    }

                                }
                                else
                                    sqlInsert("device_event", "KEYBOARD", "SINGLE", mkmapping[i, 0]);
                            }
                        }
                        isexist = true;
                        break;
                    }
                }
                if (!isexist)
                {
                    if (temp.ToString().Contains("click"))
                    {
                        if(ms_status) //type status message
                            sqlInsert("device_event", "MOUSE", "MOUSE", temp.ToString());
                    }
                    else
                    {
                        if (kb_status)
                        {


                            if (hknum != 999)
                            {
                                for (int j = 0; j < hkmapping.Length; j++)
                                {
                                    if (hkmapping[j, 2].Equals(hknum.ToString()))
                                    {
                                        sqlInsert("device_event", "KEYBOARD", hkmapping[j, 1], hkmapping[j, 0]);
                                        break;
                                    }
                                }

                            }
                            else
                                sqlInsert("device_event", "KEYBOARD", "SINGLE", temp.ToString());
                        }
                    }
                    
                }
                    
            } 
                
        }

        private void MousekeyboardEvent_task()
        {
            mktask = new Task(new Action(MousekeyboardEvent), token);
            cts = new CancellationTokenSource();
            token = cts.Token;
            mktask.Start();
        }


        //앱 목록, 프로세스 목록 갱신
        private void listRefresh()
        {

            //기존 목록 초기화
            Program_list.Clear();
          

            //실행 중인 앱 목록 획득 https://zheld.tistory.com/5
            Process[] pro = Process.GetProcesses();
            
            for (int i = 0; i < pro.Length; i++)

            {
                if (pro[i].MainWindowHandle != IntPtr.Zero)

                {
                    if (pro[i].MainWindowTitle == "")

                        continue;

                    Program_list.Add(pro[i].ProcessName);

                    
                }

            }
        }

        //Zoom 실행 중인지 확인
        private Boolean IsZoomExcuting()
        {
            listRefresh();
            if (Program_list.Contains("Zoom"))
                return true;
            else
                return false;
        }

        //화이트리스트 방식으로 실행중인 프로그램 제거 + DB에 insert
        private List<string[]> whitelist_performer()
        {
            Process[] pro = Process.GetProcesses();

            List<string[]> alist = new List<string[]>();
            int count = 0;
            for (int i = 0; i < pro.Length; i++)

            {
                if (pro[i].MainWindowHandle != IntPtr.Zero)

                {
                    if (string.IsNullOrEmpty(pro[i].MainWindowTitle))
                        continue;
                    //특정 프로그램 종료 : Zoom 과 Stop_Cheating 프로그램을 제외한 모든 프로그램 종료
                    else if (!((pro[i].ProcessName.Contains("Zoom") || pro[i].MainWindowTitle.Contains("Stop_Cheating")) || pro[i].MainWindowTitle.Contains("Argos")))
                    {
                        //pro[i].Kill();
                        if (!pro[i].MainWindowTitle.Contains("Frame"))
                        {
                            if (pro[i].CloseMainWindow())
                            {
                                string[] arr = new string[2];
                                arr[0] = pro[i].ProcessName;
                                arr[1] = pro[i].MainWindowTitle;

                                Console.WriteLine(arr[0] + " / " + arr[1]);

                                alist.Add(arr);
                                count++;
                            }
                        }
                        
                    }
                }

            }
            if (count == 0)
                return new List<string[]>();
            else
                return alist;
            
        }

        private void whitelist_Inserting(string event_process)
        {
            List<string[]> sec_third = new List<string[]>();
            sec_third = whitelist_performer();


            //별다른 프로세스 관찰되지 않음
            if (sec_third == null)
            {
                //sqlInsert("process_event", event_process, "", "");
                Console.WriteLine("별다른거 없음");
            }
            else //줌과 이 프로그램 이외의 것이 관찰됨
            {
                if (!event_process.Equals("FirstExecution"))
                {
                    for (int i = 0; i < sec_third.Count; i++)
                        sqlInsert("process_event", event_process, sec_third.ElementAt(i)[0], sec_third.ElementAt(i)[1]);
                }
               
            }



        }


        //프로세스 이벤트 탐지 https://stackoverflow.com/questions/972039/is-there-a-system-event-when-processes-are-created
        private void WaitForProcess()
        {
            try
            {
                ProcessWatcher = new ManagementEventWatcher(
              new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
                ProcessWatcher.EventArrived
                                    += new EventArrivedEventHandler(ProcessWatcher_EventArrived);
                ProcessWatcher.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString()); 
            }
        }
        //프로세스 이벤트 잡힐 시 (별도의 쓰레드임)
        private void ProcessWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            whitelist_Inserting(e.NewEvent.Properties["ProcessName"].Value.ToString());

        }

        private void FirstMonitor_Device()
        {
            string ComputerName = "localhost";
            ManagementScope Scope;
            Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", ComputerName), null);

            Scope.Connect();

            ObjectQuery Query = new ObjectQuery("SELECT * FROM Win32_PnPEntity");

            ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Scope, Query);

            int istwo = 0;

            foreach (ManagementObject device in Searcher.Get())
            {
                string temp = (string)device["Caption"];
                string temp2 = (string)device["PNPClass"];

                if (string.IsNullOrEmpty(temp))
                    Console.WriteLine("null이당");
                else if (temp.Contains("헤드폰"))
                    sqlInsert("device_event", "PnP", "FirstExecution", temp);

                if(string.IsNullOrEmpty(temp2))
                    Console.WriteLine("null이당");
                else if (temp2.Contains("Monitor"))
                {
                    istwo++;
                    if(istwo>=2)
                        sqlInsert("device_event", "PnP", "FirstExecution", temp2);
                }
            }
        }

        private void WaitForDevice()
        {
            if (DeviceWatcher == null)
                DeviceWatcher = new ManagementEventWatcher();

            WqlEventQuery weQuery = new WqlEventQuery("SELECT * FROM __InstanceOperationEvent WITHIN 1 " + "WHERE TargetInstance ISA 'Win32_PnPEntity'");
            DeviceWatcher.Query = weQuery; //Event Create 
            DeviceWatcher.EventArrived += new EventArrivedEventHandler(DeviceWatcher_EventArrived);
            DeviceWatcher.Start();
        }
        //디바이스 이벤트 잡힐 시 (별도의 쓰레드임)
        private void DeviceWatcher_EventArrived(object sender, System.Management.EventArrivedEventArgs e)
        {
            ManagementBaseObject mbo1;
            ManagementBaseObject mbo2;
            mbo1 = e.NewEvent as ManagementBaseObject;
            mbo2 = mbo1["TargetInstance"] as ManagementBaseObject;
            switch (mbo1.ClassPath.ClassName)
            {
                case "__InstanceCreationEvent":
                    {
                        strUSBDriveName = mbo2["Caption"].ToString();
                        strUSBDriveLetter = mbo2["Name"].ToString();

                        sqlInsert("device_event", "PnP", "INSERT", strUSBDriveName + " : " + strUSBDriveLetter);

                        iCount += 1;


                        break;
                    }
                case "__InstanceDeletionEvent":
                    {
                        strUSBDriveName = mbo2["Caption"].ToString();
                        strUSBDriveLetter = mbo2["Name"].ToString();

                        sqlInsert("device_event", "PnP", "REMOVE", strUSBDriveName + " : " + strUSBDriveLetter);

                        iCount += 1;

                        break;
                    }
            }
            this.Refresh();
        }

        private void AudioTracker()
        {
        
            Process p = new Process();
            ProcessStartInfo pp = new ProcessStartInfo();

            pp.FileName = @"C:\ArgosAjou\speech_recog.exe";
            pp.WindowStyle = ProcessWindowStyle.Minimized;
            p.StartInfo = pp;
            p.Start();

            Thread.Sleep(1000); //sleep for a second

            p.Refresh();
            try
            {
                SetParent(p.MainWindowHandle, audio_panel.Handle);
                SetWindowLong(p.MainWindowHandle, GWL_STYLE, WS_VISIBLE);
                MoveWindow(p.MainWindowHandle, 0, 0, audio_panel.Width, audio_panel.Height, true);
            }
            catch(Exception e)
            {
                MessageBox.Show("프로세스 합성 실패!");
                exit_shutdown();
            }
            
            
        }

        private void VideoTracker()
        {
            Process p = new Process();
            ProcessStartInfo pp = new ProcessStartInfo();

            pp.FileName = @"C:\ArgosAjou\eyetracking0530.exe";
            pp.WindowStyle = ProcessWindowStyle.Minimized;
            p.StartInfo = pp;
            p.Start();

            Thread.Sleep(1000); //sleep for a second

            p.Refresh();

            try
            {
                SetParent(p.MainWindowHandle, video_panel.Handle);
                SetWindowLong(p.MainWindowHandle, GWL_STYLE, WS_VISIBLE);
                MoveWindow(p.MainWindowHandle, 0, 0, video_panel.Width, video_panel.Height, true);
            }
            catch (Exception e)
            {
                MessageBox.Show("프로세스 합성 실패!");
                exit_shutdown();
            }
           

            Console.WriteLine();

            /*Thread.Sleep(15000); //sleep for 3 seconds
            Process[] pro = Process.GetProcesses();

            for (int i = 0; i < pro.Length; i++)

            {
                if (pro[i].MainWindowHandle != IntPtr.Zero)

                {
                    if (pro[i].MainWindowTitle == "")
                        continue;
                    Console.WriteLine(pro[i].ProcessName);
                    if (pro[i].ProcessName == "practice1")
                    {
                        Console.WriteLine(pro[i].MainWindowTitle);
                        Thread.Sleep(1000); //sleep for 3 seconds
                        SetParent(pro[i].MainWindowHandle, cam_panel.Handle);
                        SetWindowLong(pro[i].MainWindowHandle, GWL_STYLE, WS_VISIBLE);
                        MoveWindow(pro[i].MainWindowHandle, 0, 0, cam_panel.Width, cam_panel.Height, true);
                        Console.WriteLine("캠 장착 완료");
                        break;
                    }
                }

            }*/
        }

        private void WaitForFile()
        {
            FileWatcher = new FileSystemWatcher();
            FileWatcher.Path = @"C:\ArgosAjou";  //2. 감시할 폴더 설정(디렉토리)

            // 3. 감시할 항목들 설정 (파일 생성, 크기, 이름., 마지막 접근 변경등..)
            FileWatcher.NotifyFilter = NotifyFilters.Size;

            //감시할 파일 유형 선택 예) *.* 모든 파일
            FileWatcher.Filter = "*.*";

            FileWatcher.IncludeSubdirectories = true;

            // 4. 감시할 이벤트 설정 (생성, 변경..)
            FileWatcher.Changed += new FileSystemEventHandler(sizeChanged);
           
            // 5. FIleSystemWatcher 감시 모니터링 활성화
            FileWatcher.EnableRaisingEvents = true;
        }
        private void sizeChanged(object source, FileSystemEventArgs e) {

            FileStream fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); 
            StreamReader sr = new StreamReader(fs, Encoding.Default);

            string content = sr.ReadToEnd();
            Console.WriteLine(content);
            if (e.FullPath.Contains("video"))
            {
                string[] msg = content.Split('+');
                if (msg[1].Contains("1"))
                {
                    msg[1] = "SHORT_TIME";
                }
                else if (msg[1].Contains("2"))
                {
                    msg[1] = "LONG_TIME";
                }
                else if (msg[1].Contains("3"))
                {
                    msg[1] = "";
                }
                else if (msg[1].Contains("4"))
                {
                    msg[1] = "";
                }
                else if (msg[1].Contains("5"))
                {
                    msg[1] = "FACE_NOT_FOUND";
                }
                else if (msg[1].Contains("6"))
                {
                    msg[1] = "AUTH_RESULT";
                    Tray_icon.BalloonTipText = msg[0];
                    Tray_icon.BalloonTipTitle = "신원인증 성공!";
                    Tray_icon.ShowBalloonTip(8000);
                }
                else if (msg[1].Contains("7"))
                {
                    msg[1] = "ANOTHER_FACE_DETECT";
                }
                
                sqlInsert("device_event", "eyetracking", msg[1], msg[0]);
            }
            else if(e.FullPath.Contains("audio"))
            {
                sqlInsert("device_event", "AUDIO", "AUDIO", content);
            }

        }

        private void Zoom_start_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            try
            {
                p = Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Zoom");
            }
            catch (Exception ee)
            {
                MessageBox.Show("Zoom을 실행하십시오.", "Failed find Zoom");
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            if (!Detectsuccess)
            {
                //현재 쓰레드 종료
                this.Close();
                Thread th = new Thread(restarting);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        private void restarting(object obj)
        {
           Process p = Process.Start(".\\Argos.exe");
           exit_shutdown();

        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit_shutdown();
        }

        private void Student_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Tray_icon.Visible = true;
                this.Hide();
                e.Cancel = true;
            }
        }

        private void metroTabControl1_Selected(object sender, TabControlEventArgs e)
        {
            
            if (!Loginsuccess && !metroTabControl1.SelectedTab.Equals(LoginPage))
            {
                MessageBox.Show("로그인이 완료되지 않았습니다. ", "Login is not completed");
                metroTabControl1.SelectedTab = LoginPage;
            }
            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            string nowtime = DateTime.Now.ToString().Split(' ')[2];
            label11.Text = nowtime;


        }

     
    }
}

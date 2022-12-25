using System;
using System.Drawing;
namespace finalproject
{
    class Program
    {
        //任務struct
        struct task{
            public string task_name;
            public string task_discription;
            public string task_form;
            public bool task_complete;
            public bool task_activate;
            }
        //物品struct
        struct item{
            public string item_name;
            public int item_codename;
            public string item_intro;
            public int item_num;


        }
            static void Main(string[] args)
        {
            //開場          
            Console.WriteLine("無人島的生存日記\n請按enter鍵繼續");
            Console.ReadKey();
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine("你是一位常在海上航行的冒險家");
            Console.ReadKey();
            Console.WriteLine("在一次航行中遇到了暴風雨");
            Console.ReadKey();
            Console.WriteLine("你的船因為狂風大浪失去了控制");
            Console.ReadKey();
            Console.WriteLine("突然一震猛烈的衝擊使你失去的意識");
            Console.ReadKey();
            Console.WriteLine("醒過來的時候發現你已經在一個無人的沙灘上");
            Console.ReadKey();
            Console.WriteLine("好好活下去吧");
            Console.ReadLine();
            //基本變數宣告
            int time=10;//回合係數
            int l1_command;//第一層命令
            int day=1;//日數計時器
            int i;
            int wannaeat;
            //人物屬性
            int life=100;
            int hunger=100;        
            //任務欄
            task [] Task=new task[20];
                //任務1:營火
                Task[1].task_name="搭建營火\t";
                Task[1].task_discription="搭建一個營火\t";
                Task[1].task_form=("......未完成");
                Task[1].task_complete=false;
                Task[1].task_activate=true;
                //任務2:水源
                Task[2].task_name="尋找水源\t";
                Task[2].task_discription="在河川位置探索一次\t";
                Task[2].task_form=("......未完成");
                Task[2].task_complete=false;
                Task[2].task_activate=true;
                //任務3:狼煙
                Task[3].task_name="升起救援狼煙\t";
                Task[3].task_discription="在山上建立物品「狼煙」\t";
                Task[3].task_form=("......未完成");
                Task[3].task_complete=false;
                Task[3].task_activate=false;//第二天開始時出現，達成結局「救援成功」必要條件，10天內存活並達成此任務
                //任務4:逃離
                Task[4].task_name="準備逃離無人島\t";
                Task[4].task_discription="準備好必要道具「逃生船」，食物*10，並抵達海岸\t";
                Task[4].task_form=("......未完成");
                Task[4].task_complete=false;
                Task[4].task_activate=false;//當完成「木筏」物品製作時出現，達成結局「逃出生天」必要條件
                //任務5:神秘力量
                Task[5].task_name="研究「神秘圓球」\t";
                Task[5].task_discription="在晚上8點之後到達「森林」，小心可能來臨的考驗\t";
                Task[5].task_form=("......未完成");
                Task[5].task_complete=false;
                Task[5].task_activate=false;//在第5天的8點之後在山上探索時發現，達成結局「神秘力量」必要條件，在晚上8點後到達森林探索可觸發神秘山洞劇情，將遭遇強大的野獸，攜帶任何武器即可擊敗。
           
            
            //背包
            item [] package= new item[30];

           /*測試 救援成功結局
            int find_thing;
            find_thing='木';                      
            store(ref find_thing,ref package);
            find_thing='石';                      
            store(ref find_thing,ref package);
            find_thing='葉';                      
            store(ref find_thing,ref package);
            find_thing='肉';                      
            store(ref find_thing,ref package);
            find_thing='皮';                      
            store(ref find_thing,ref package);
            find_thing='果';                      
            store(ref find_thing,ref package);
            find_thing='草';                      
            store(ref find_thing,ref package);
            find_thing='繩';                      
            store(ref find_thing,ref package);
            */
            
            //地點
            string []map={"0.營地","1.海岸","2.山上","3.森林","4.河岸"};
            int location=0;
            //控制面板
            
            while(true){
            basic_board:
            Console.ForegroundColor=ConsoleColor.White;          
            Console.WriteLine("\n今天是你在無人島上的第{0}天",day);
            if(hunger<=0){
                hunger=0;
            }
            Console.WriteLine("人物屬性:\n生命值:"+life+"\t飽食度:"+hunger);
            Console.WriteLine("現在所在位置為:"+map[location]);
            Console.WriteLine("現在時間為:"+time+"\n");
            Console.ResetColor();
            hugercounter(ref hunger,ref life);              //飢餓度計算器
            end02(ref location,ref package,ref life,ref hunger,ref time,ref day,ref Task);
                                                     
            
                //狼煙任務出現
                if(day>=2&Task[3].task_activate==false){
                    Console.ForegroundColor=ConsoleColor.Red;
                    Console.WriteLine("你有新任務\n");
                    Task[3].task_activate=true;

            }
            //////////////////////////////////////////
            //////////////////////////////////////////
            //////////////////////////////////////////end逃出生天
            bool haveboat=false;
            bool havefood=false;
            for(int j=0;j<package.Length;j++){              //是否有船
                        if(package[j].item_codename=='船'){
                            haveboat=true;
                        }
                    }
            for(int meat=0;meat<package.Length;meat++){              //是否夠食物
                        if(package[meat].item_codename=='肉'){
                            for(int fruit=0;fruit<package.Length;fruit++){              //是否夠食物
                        if(package[fruit].item_codename=='果'){
                            for(int weed=0;weed<package.Length;weed++){              //是否夠食物
                        if(package[weed].item_codename=='草'){
                            if((meat*4+weed*2+fruit*2)>=10){
                                havefood=true;
                            }
                        }
                    }
                        }
                    }
                            
                        }
                    }

            
            if(location==1&haveboat){
                end02:
                Console.WriteLine("你已經準備好逃生船，是否離開這座島? Y/N");                    
                                        char answer;
                                        try{    
                                            answer=Convert.ToChar(Console.ReadLine());
                                        }catch(System.FormatException){
                                            Console.WriteLine("請輸入正確的指令!");
                                            Console.ReadKey();
                                            goto end02;
                                        }
                                            if(answer=='Y'|answer=='y'){
                                                Console.ForegroundColor=ConsoleColor.Blue;
                                                if(havefood==true){
                                                    Console.WriteLine("你搭上了小船，緩緩地駛離了這座島");
                                                    Console.ReadKey();
                                                    Console.WriteLine("幸運的是一路上風平浪靜，而且好家在你有準備夠多的食物");
                                                    Console.ReadKey();
                                                    Console.WriteLine("在食物見底之前，你被一艘路過的漁船發現，成功的獲救!");
                                                    Console.ForegroundColor=ConsoleColor.Red;
                                                    Console.WriteLine("達成結局「逃出生天」");
                                                    Console.ReadKey();
                                                    Console.WriteLine("恭喜逃出無人島!");
                                                    Console.ReadKey();
                                                    Console.WriteLine("Thanks fo playing....");
                                                    Console.ReadKey();
                                                    Console.WriteLine("作者：林正翌");
                                                    Console.ReadKey();
                                                    Console.WriteLine("按下任何鍵結束遊戲...");
                                                    Console.ReadKey();
                                                    System.Environment.Exit(0);
                                                                                             
                                                }else{
                                                    Console.WriteLine("你搭上了小船，緩緩地駛離了這座島");
                                                    Console.ReadKey();
                                                    Console.WriteLine("雖然一路上風平浪靜，但是食物很快就見底了，你的希望越來越渺茫......");
                                                    Console.ReadKey();
                                                    Console.ForegroundColor=ConsoleColor.Red;
                                                    Console.WriteLine("遊戲結束...");
                                                    Console.ReadKey();
                                                    System.Environment.Exit(0);                                                    
                                                }

                                            }else if(answer=='n'|answer=='N'){
                                                Console.WriteLine("你覺得時機還沒到~");
                                                goto l1_command;
                                            }else{
                                                Console.WriteLine("請輸入正確的指令!");
                                                goto end02;

                                            }

            }
            /////第一層命令         
            l1_command:
            Console.ForegroundColor=ConsoleColor.White;
            Console.WriteLine("請選擇動作:\n1.移動位置\t2.查看任務\t3.查看背包\t4.攝取食物\t5.探索地區\t6.建造物品\n");
            
            try{
                l1_command = Convert.ToInt32(Console.ReadLine());                
                }catch(System.FormatException){
                        Console.WriteLine("\n>>>系統提示：請輸入正確指令\n");
                        goto l1_command;
                        }
                  

                /////移動命令
                move_phase:
                if(l1_command==1){
                            Console.WriteLine("要移動到哪裡?:");
                         for(i=0;i<=4;i++){                              //顯示除了現在地點之外的位置
                            if(i==location){
                            continue;
                            }
                            Console.Write(map[i]+"\t");
                            }
                   
                    try{
                        location=Convert.ToInt32((Console.ReadLine()));
                        if (location>4|location<0){
                            Console.WriteLine("\n>>>系統提示：請輸入正確指令\n");
                            goto move_phase;
                             }
                        }
                    catch(System.FormatException){
                        Console.WriteLine("\n>>>系統提示：請輸入正確指令\n");
                        goto move_phase;
                        }
                    hunger= hunger-10;                              //飢餓度-10
                    timecounter(ref time,ref day,ref location,ref Task,ref package,ref life);     //時間計數器
                   
                
                    }else if(l1_command==2)
                ////任務命令                
                    {
                        Console.WriteLine("\n");
                        taskcommand(ref Task); 
                        Console.WriteLine("\n"); 
                    }else if(l1_command==3){
                ////背包命令
                        Console.WriteLine("\n");
                        backpack(ref package);
                        Console.WriteLine("\n");

                    }else if(l1_command==4){
                ////進食命令
                bool havemeat=false;
                bool havefruilt=false;
                bool haveseaweed=false;
                for(int k=0;k<package.Length;k++){
                        if(package[k].item_codename=='肉'&package[k].item_num>0){                                                                       /////判斷有無食物
                            havemeat=true;
                        }else if(package[k].item_codename=='果'&package[k].item_num>0){
                            havefruilt=true;
                        }else if(package[k].item_codename=='草'&package[k].item_num>0){
                            haveseaweed=true;
                        }                
                    }
                if(havemeat==false&havefruilt==false&haveseaweed==false){
                    Console.WriteLine("完全沒有食物......");
                    goto basic_board;
                }
                    
                    for(int k=0;k<package.Length;k++){
                        if(package[k].item_codename=='肉'){                                                                       /////秀出現有食物
                            Console.WriteLine("1."+package[k].item_name+"\t"+package[k].item_num+"\t"+package[k].item_intro+"\n");
                        }                
                    }
                    for(int k=0;k<package.Length;k++){
                        if(package[k].item_codename=='果'){                                                                       /////秀出現有食物
                            Console.WriteLine("2."+package[k].item_name+"\t"+package[k].item_num+"\t"+package[k].item_intro+"\n");
                        }                
                    }
                    for(int k=0;k<package.Length;k++){
                        if(package[k].item_codename=='草'){                                                                       /////秀出現有食物
                            Console.WriteLine("3."+package[k].item_name+"\t"+package[k].item_num+"\t"+package[k].item_intro+"\n");
                        }                
                    }
                    Console.WriteLine("今晚，我想來點......");
                    Console.WriteLine("輸入欲進食的代碼");
                    wannaeat=Convert.ToInt32(Console.ReadLine());
                        if (wannaeat==1){
                                for(int k=0;k<package.Length;k++){
                                    if(package[k].item_codename=='肉'){                                                                       /////秀出現有食物
                                        if(package[k].item_num>0){
                                            Console.WriteLine("攝取了肉塊，飽食度+40");
                                            package[k].item_num=package[k].item_num-1;
                                            hunger=hunger+40;
                                            Console.ReadKey();
                                            timecounter(ref time,ref day,ref location,ref Task,ref package,ref life);                        
                                            deadoralive(life);  
                                            goto basic_board;
                                        }else{
                                            Console.WriteLine("肉塊不足");
                                            Console.ReadKey();
                                            goto basic_board;
                                                                                
                                        }
                                        }else{                                            
                                            
                                        }
                                         
                                 }
                                 Console.WriteLine("肉塊不足");
                                        Console.ReadKey();
                                        break;   
                            
                        }else if(wannaeat==2){
                            for(int k=0;k<package.Length;k++){
                                    if(package[k].item_codename=='果'){                                                                       /////秀出現有食物
                                        if(package[k].item_num>0){
                                            Console.WriteLine("攝取了水果，飽食度+20");
                                            package[k].item_num=package[k].item_num-1;
                                            hunger=hunger+20;
                                            Console.ReadKey();
                                            timecounter(ref time,ref day,ref location,ref Task,ref package,ref life);                        
                                            deadoralive(life);  
                                            goto  basic_board;
                                        }else{
                                            Console.WriteLine("水果不足");
                                            Console.ReadKey();
                                            goto  basic_board;
                                                                                        
                                        }
                                        }else{
                                                                                   
                                        }                                          
                                 }
                                 Console.WriteLine("水果不足");
                                        Console.ReadKey();  
                                        break;

                        }else if(wannaeat==3){
                            for(int k=0;k<package.Length;k++){
                                    if(package[k].item_codename=='草'){                                                                       /////秀出現有食物
                                        if(package[k].item_num>0){
                                            Console.WriteLine("攝取了海草，飽食度+20");
                                            package[k].item_num=package[k].item_num-1;
                                            hunger=hunger+20;
                                            Console.ReadKey();
                                            timecounter(ref time,ref day,ref location,ref Task,ref package,ref life);                        
                                            deadoralive(life);
                                            goto basic_board;  
                                            
                                        }else{
                                            Console.WriteLine("海草不足");
                                            Console.ReadKey();
                                            goto basic_board;
                                                                                     
                                        }
                                        }else{
                                                                                       
                                        }
                                        
                                 }
                                 Console.WriteLine("海草不足");
                                        Console.ReadKey();  
                                        break; 
                        }else {
                            Console.WriteLine("\n>>>系統提示：請輸入正確指令\n");
                            goto basic_board;
                        }
                        


                
                    }else if(l1_command==5){
                ////探索命令
                        search(ref location,ref package,ref life,ref hunger,ref time,ref day,ref Task);
                        timecounter(ref time,ref day,ref location,ref Task,ref package,ref life);                        
                        deadoralive(life);
                    }else if(l1_command==6){
                ////建造命令
                        build(ref location,ref package,ref life,ref hunger,ref time,ref day,ref Task); 
                        for(int k=0;k<package.Length;k++){              //營火任務判斷
                                    if(package[k].item_codename=='火'){                                                                       /////秀出現有食物
                                        if(package[k].item_num>0){
                                            Task[1].task_complete=true;
                                            Task[1].task_form="......已完成";
                                            break;
                                            }
                                     }
            }                           
                        timecounter(ref time,ref day,ref location,ref Task,ref package,ref life);                        
                        deadoralive(life);           
                    
                    }else{
                        Console.WriteLine("\n>>>系統提示：請輸入正確指令\n");
                        goto l1_command;
                    }
                


            }   

        }
        //存活判斷
        static void deadoralive(int life)
        {
            if (life<=0){
                Console.WriteLine("\n遊戲結束");
                Console.ReadLine();
                System.Environment.Exit(0);
                
                
            }
            
        }
        //時間函式
        static void timecounter(ref int time,ref int day,ref int location,ref task [] Task,ref item [] package,ref int life){
            ++time;
            if (time==22){
            Console.ReadKey();
            Console.Clear();
            day++;
            location=0;
           if(Task[1].task_complete==false){                                                                   ////判斷營火任務是否完成並且重設
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("夜幕低垂，沒有營火照耀夜晚的你，漸漸地迷失了方向......");
                Console.ReadKey();
                Console.WriteLine("遊戲結束");
                Console.ReadKey();
                 System.Environment.Exit(0);
            }else{
                Console.ForegroundColor=ConsoleColor.Blue;
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("\n夜幕低垂，經過了一日的奮鬥，你的生命在這原始之島上又掙扎了一天\n");
                Console.WriteLine("\n生命值+50\n");
                Console.ReadKey();
                life=life+50;
                if (life>100){
                    life=100;
                }
                for(int k=0;k<package.Length;k++){
                                    if(package[k].item_codename=='火'){                                                                       /////秀出現有食物
                                        if(package[k].item_num>0){                                            
                                            package[k].item_num=package[k].item_num-1;
                                            Console.ReadKey();  
                                            break;
                                        }
                                    }
                }
            }
            Task[1].task_form=("......未完成");
            Task[1].task_complete=false;
            time=8;

        }
        

            
        }
        //飢餓度涵式
        static void hugercounter(ref int hunger,ref int life){
            
            if(hunger<=0){
                life=life-10;
                if (life>0){
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("\n你的已經無法忽視你的飢餓感，盡快找東西吃!\n");
            }else{
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("\n你餓死了......\n");
            }
            deadoralive(life);
            }


        }
        //任務涵式
        static void taskcommand(ref task[] Task){
            for(int i=0;i<Task.Length;i++){
                if(Task[i].task_activate==true){
                    Console.WriteLine(Task[i].task_name+Task[i].task_discription+Task[i].task_form);
                }else{
                    continue;
                }
            }

        }
        //背包涵式
        static void backpack(ref item[] package){
                for(int i=0;i<package.Length;i++){
                    if(package[i].item_codename==0){
                        continue;

                    }else{
                
                    Console.WriteLine(package[i].item_name+"\t"+package[i].item_num+"\t"+package[i].item_intro+"\n");
                    }
            }

        }
        //存放物品涵式
        static void store(ref int thing,ref item[] package){

                switch(thing)
                {
                    case '木':
                                        
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='木'){                     //判斷陣列裡面的物件是否為木        
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 //如果是空的，新增木頭的欄位                  可作為建造的參考迴圈判斷陣列內是否為材料，扣除材料後在背包新增製作的物件
                            package[i].item_name="木頭";
                            package[i].item_codename='木';
                            package[i].item_intro="沒什麼特別的，就是塊木頭";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                //否則搜尋下一個位置
                                continue;
                            }
                        }
                            break;
                    case '石':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='石'){                     //石頭
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="石頭";
                            package[i].item_codename='石';
                            package[i].item_intro="比你腦袋瓜還硬的東西";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }       
                            break;
                    case '葉':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='葉'){                     //葉
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="棕梠葉";
                            package[i].item_codename='葉';
                            package[i].item_intro="綠色是原諒的顏色";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }       
                            break;
                    case '肉':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='肉'){                     //肉
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="肉塊";
                            package[i].item_codename='肉';
                            package[i].item_intro="肉多多~肉多多~------飽食度+40";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }       
                            break;
                    case '皮':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='皮'){                     //毛皮
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="毛皮";
                            package[i].item_codename='皮';
                            package[i].item_intro="毛毛的東西";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }    
                            break;
                    case '果':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='果'){                     //水果
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="水果";
                            package[i].item_codename='果';
                            package[i].item_intro="去菜市場看黑人呷水果啦!-------飽食度+20";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }    
                            break;
                    case '沙':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='沙'){                     //沙子
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="沙子";
                            package[i].item_codename='沙';
                            package[i].item_intro="閃閃發光";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }    
                            break;
                    case '草':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='草'){                     //海草
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="海草";
                            package[i].item_codename='草';
                            package[i].item_intro="傳說中對頭髮很好-------飽食度+20";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }    
                            break;
                    case '竿':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='竿'){                     //魚竿
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="魚竿";
                            package[i].item_codename='竿';
                            package[i].item_intro="你現在可以在海岸跟河岸釣魚了!";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }    
                            break;
                    case '矛':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='矛'){                     //長矛
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="長矛";
                            package[i].item_codename='矛';
                            package[i].item_intro="古老的武器，打架的時候減少生命值損失";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                    case '球':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='球'){                     //神秘球體
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="神秘球體";
                            package[i].item_codename='球';
                            package[i].item_intro="森林裡撿到的奇怪球球，微微的發著光";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                    case '阱':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='阱'){                     //陷阱
                            package[i].item_num=package[i].item_num+1;
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="補兔陷阱";
                            package[i].item_codename='阱';
                            package[i].item_intro="在森林中可以捕到兔子了";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                    case '火':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='火'){                     //營火
                            package[i].item_num=package[i].item_num+1;                            
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="營火";
                            package[i].item_codename='火';
                            package[i].item_intro="你可以平安的度過夜晚了，每一天都會消耗一個營火";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                    case '繩':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='繩'){                     //繩子
                            package[i].item_num=package[i].item_num+1;                            
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="繩子";
                            package[i].item_codename='繩';
                            package[i].item_intro="別把它看成蛇嚕~";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                    case '煙':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='煙'){                     //狼煙
                            package[i].item_num=package[i].item_num+1;                            
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="狼煙";
                            package[i].item_codename='煙';
                            package[i].item_intro="很嗆麻";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                    case '帆':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='帆'){                     //船帆
                            package[i].item_num=package[i].item_num+1;                            
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="船帆";
                            package[i].item_codename='帆';
                            package[i].item_intro="晚上可以拿來當棉被";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                    case '槳':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='槳'){                     //船槳
                            package[i].item_num=package[i].item_num+1;                            
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="船槳";
                            package[i].item_codename='槳';
                            package[i].item_intro="槳槳槳槳~";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                    case '板':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='板'){                     //船板
                            package[i].item_num=package[i].item_num+1;                            
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="船板";
                            package[i].item_codename='板';
                            package[i].item_intro="砍樹砍得很累吧";
                            package[i].item_num=package[i].item_num+1;
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                    case '船':
                    for(int i=0;i<package.Length;i++){
                        if(package[i].item_codename=='船'){                     //逃生船
                            package[i].item_num=package[i].item_num+1;                            
                            break;      
                            }else if(package[i].item_codename==0){                 
                            package[i].item_name="逃生船";
                            package[i].item_codename='船';
                            package[i].item_intro="終於可以離開這該死的島了";
                            package[i].item_num=package[i].item_num+1;                            
                            break;
                                }else {                                                
                                continue;
                            }
                        }
                        break;
                       }
        }
    /*////探索函式///1.判斷現在位置
                    2.switch決定機率
                    3.呼叫存放函式*/
        static void search(ref int location,ref item[] package,ref int life,ref int hunger,ref int time,ref int day,ref task [] Task){
                Random rand=new Random();
                int find_thing;
                bool fishing=false;                         //是否可以釣魚判定
                
                switch(location)
                {   
                    //////////////////////////////////////////////////////////////////////海岸
                   case 1:     
                    for(int j=0;j<package.Length;j++){              //是否可以釣魚判定
                        if(package[j].item_codename=='竿'){
                            fishing=true;
                        }
                    }
                   if(fishing==false){                              //釣魚竿有無
                   for(int i=0;i<5;i++){
                    int dice_1=rand.Next()%100;
                    if(dice_1>=0&dice_1<40){
                        find_thing='木';                 
                        Console.WriteLine("\n找到木頭x1");
                        Console.ReadKey();  
                        store(ref find_thing,ref package);
                        }else if(dice_1>=40&dice_1<80){
                        find_thing='石';      
                        Console.WriteLine("\n找到石頭x1");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        }else if(dice_1>=80&dice_1<100){
                        find_thing='草';      
                        Console.WriteLine("\n找到海草x1");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        }
                     }
                    }else{
                    for(int i=0;i<5;i++){
                    int dice_1=rand.Next()%100;
                    if(dice_1>=0&dice_1<30){
                        find_thing='木';                 
                        Console.WriteLine("\n找到木頭x1");
                        Console.ReadKey();  
                        store(ref find_thing,ref package);
                        }else if(dice_1>=30&dice_1<60){
                        find_thing='石';      
                        Console.WriteLine("\n找到石頭x1");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        }else if(dice_1>=60&dice_1<80){
                        find_thing='草';      
                        Console.WriteLine("\n找到海草x1");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        }else if(dice_1>=80&dice_1<100){
                        find_thing='肉';      
                        Console.WriteLine("\n釣到魚了，肉塊x2");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        store(ref find_thing,ref package);
                        }
                     }
                    }
                    hunger=hunger-10;
                    hugercounter(ref hunger,ref life);
                    break;
                    //////////////////////////////////////////////////////////////////////山上
                    case 2:
                    bool havespear = false;
                    for(int j=0;j<package.Length;j++){              //是否擁有武器判定
                        if(package[j].item_codename=='矛'){
                            havespear=true;
                        }
                    }
                        for(int i=0;i<7;i++){
                            int dice_1=rand.Next()%100;
                                 if(dice_1>=0&dice_1<40){
                                    find_thing='木';                 
                                    Console.WriteLine("\n找到木頭x1");
                                    Console.ReadKey();  
                                     store(ref find_thing,ref package);
                                    }else if(dice_1>=40&dice_1<80){
                                        find_thing='石';      
                                        Console.WriteLine("\n找到石頭x1");
                                        Console.ReadKey();                                      
                                        store(ref find_thing,ref package);
                                    }else if(dice_1>=80&dice_1<100){
                                        fight_pig:
                                        Console.WriteLine("\n你遇到了一隻兇猛的野豬，是否跟他決鬥 Y/N");
                                        char answer;
                                        try{    
                                            answer=Convert.ToChar(Console.ReadLine());
                                        }catch(System.FormatException){
                                            Console.WriteLine("請輸入正確的指令!");
                                            Console.ReadKey();
                                            goto fight_pig;
                                        }
                                            if(answer=='Y'|answer=='y'){
                                                if(havespear==false){
                                                    Console.WriteLine("勇猛如你空著手跟野豬對決，終究是贏了，獲得肉塊X4，毛皮x2 生命值-10");
                                                    Console.ReadKey();
                                                    life=life-10;    
                                                    find_thing='肉';                           
                                                    store(ref find_thing,ref package);
                                                    store(ref find_thing,ref package); 
                                                    store(ref find_thing,ref package); 
                                                    store(ref find_thing,ref package);
                                                    find_thing='皮';
                                                    store(ref find_thing,ref package); 
                                                    store(ref find_thing,ref package);                                         
                                                }else{
                                                    Console.WriteLine("你使用武器痛宰了野豬，獲得肉塊X4，毛皮x2 生命值-5");
                                                    life=life-5;    
                                                    find_thing='肉';
                                                    Console.ReadKey();                           
                                                    store(ref find_thing,ref package);
                                                    store(ref find_thing,ref package); 
                                                    store(ref find_thing,ref package); 
                                                    store(ref find_thing,ref package);
                                                    find_thing='皮';
                                                    store(ref find_thing,ref package); 
                                                    store(ref find_thing,ref package); 
                                                }

                                            }else if(answer=='n'|answer=='N'){
                                                Console.WriteLine("塊陶阿~");
                                                continue;
                                            }else{
                                                Console.WriteLine("請輸入正確的回答!");
                                                goto fight_pig;

                                            }
                                    }
                        }
                    hunger=hunger-20;
                    hugercounter(ref hunger,ref life);
                    break;
                    //////////////////////////////////////////////////////////////////////森林
                    case 3:
                    bool havetrap=false;
                    for(int j=0;j<package.Length;j++){              //是否擁有陷阱判定
                        if(package[j].item_codename=='阱'){
                            havetrap=true;
                        }
                    }
                    if(havetrap==false){                              //釣魚竿有無
                        for(int i=0;i<5;i++){
                         int dice_1=rand.Next()%100;
                            if(dice_1>=0&dice_1<40){
                                find_thing='木';                 
                                Console.WriteLine("\n找到木頭x1");
                                Console.ReadKey();  
                                store(ref find_thing,ref package);
                                }else if(dice_1>=40&dice_1<80){
                                    find_thing='葉';      
                                    Console.WriteLine("\n找到棕梠葉x1");
                                    Console.ReadKey();                                      
                                    store(ref find_thing,ref package);
                                }else if(dice_1>=80&dice_1<100){
                                    find_thing='果';      
                                    Console.WriteLine("\n找到水果x1");
                                    Console.ReadKey();                                      
                                    store(ref find_thing,ref package);
                                }
                        }
                    }else{
                        for(int i=0;i<5;i++){
                            int dice_1=rand.Next()%100;
                            if(dice_1>=0&dice_1<30){
                                find_thing='木';                 
                                Console.WriteLine("\n找到木頭x1");
                                Console.ReadKey();  
                                store(ref find_thing,ref package);
                                }else if(dice_1>=30&dice_1<60){
                                    find_thing='葉';      
                                    Console.WriteLine("\n找到棕梠葉x1");
                                    Console.ReadKey();                                      
                                    store(ref find_thing,ref package);
                                }else if(dice_1>=60&dice_1<80){
                                    find_thing='果';      
                                    Console.WriteLine("\n找到水果x1");
                                    Console.ReadKey();                                      
                                    store(ref find_thing,ref package);
                                }else if(dice_1>=80&dice_1<100){
                                    find_thing='肉';      
                                    Console.WriteLine("\n抱歉了兔兔，我真的很需要你身上的酷東西，肉塊X2，毛皮X1");
                                    Console.ReadKey();                                      
                                    store(ref find_thing,ref package);
                                    store(ref find_thing,ref package);
                                    find_thing='皮';
                                    store(ref find_thing,ref package);
                                }                            
                        }
                    }
                    ////////////
                    ////////////
                    ////////神秘圓球劇情
                    bool haveball=false;
                    for(int j=0;j<package.Length;j++){              //是否擁有球球判定
                        if(package[j].item_codename=='球'){
                            haveball=true;
                        }
                    }
                    if(day==5&time>=20){
                        if(haveball==true){

                        }else{
                        find_thing='球';
                        Console.ForegroundColor=ConsoleColor.Red;
                        Console.WriteLine("在黑暗中，你注意到草叢中有一樣發光的物品，獲得「奇怪發光圓球」");
                        store(ref find_thing,ref package);
                        Console.ForegroundColor=ConsoleColor.Red;
                        Console.WriteLine("你有新任務\n");
                        Task[5].task_activate=true;
                        hunger=hunger-10;
                        hugercounter(ref hunger,ref life);
                        return;//跳出函式 
                        }                    
                    }
                   
                    
                    if(haveball==true&time>=20){                     //找到神秘山洞
                    Console.ForegroundColor=ConsoleColor.Blue;   
                        Console.WriteLine("你背包中的神秘球體開始發亮，並衝出了你的背包，跟隨著它前進，你到達了一個從未看過的岩壁，岩壁上刻畫著奇怪的圖騰，中央有一個剛好符合球體的圓形窟窿......");
                        Console.ReadKey();
                        theend :
                        Console.WriteLine("要鑲入球體嗎? Y/N (注意!輸入Y即立刻觸發「神秘洞窟」劇情，確保你的裝備夠齊全)");
                        char answer2;
                        answer2=Convert.ToChar(Console.ReadLine());
                        Console.Clear();
                        if(answer2=='Y'|answer2=='y'){
                            Console.WriteLine("你將球體鑲入了岩壁上，球體的光芒慢慢地延伸到了整個岩壁，接著一陣晃動，\n岩壁緩緩地上升，在你眼前的是一個通道");
                            Console.ReadKey();
                            Console.WriteLine("你緩緩的往裡面前進，通道越來越寬敞，最後你來到了一個房間，\n房間中央有一個巨大的怪物石像......");
                            Console.ReadKey();
                            Console.WriteLine("就在你思考著這一切的時候，石像開始劇烈的搖動並發出巨大的聲響，接著它睜開了眼睛，向你撲了過來");
                            Console.ReadKey();
                            havespear = false;
                            for(int j=0;j<package.Length;j++){              //是否擁有武器判定
                                if(package[j].item_codename=='矛'){
                                havespear=true;
                                }
                            }
                            if(havespear==true){            //判斷是否有武器
                            Console.WriteLine("你奮力地和它搏鬥，一次又一次的閃過它銳利的爪子，最後逮到了機會，用力地將手中的武器砸向它背上的水晶");
                            Console.ReadKey();
                            Console.WriteLine("石像爆裂成碎片，光芒點亮了房間的每個角落......");
                            Console.ReadKey();
                            Console.WriteLine("光芒散去後，你發現地板上閃耀著奇怪的魔法陣，神秘的圓球漂浮在中心，你好奇地走向圓球......");
                            Console.ReadKey();
                            Console.WriteLine("在你接觸到圓球時，一陣強大的引力把你吸住，魔法陣開始發光......接著你失去的意識");
                            Console.ReadKey();
                            Console.ForegroundColor=ConsoleColor.Red;
                            Console.WriteLine("達成結局「神秘力量」");
                            Console.WriteLine("恭喜逃出無人島!");
                            Console.WriteLine("Thanks fo playing....");
                            Console.WriteLine("作者：林正翌");
                            Console.WriteLine("按下任何鍵結束遊戲...");
                            Console.ReadKey();
                            System.Environment.Exit(0);
                            }else{
                            Console.WriteLine("你奮力地和它搏鬥，不過在沒有武器的情況下實在是找不到機會給予致命一擊......遍體麟傷的你漸漸地失去意識......");
                            Console.ReadKey();
                            life=life-100;
                            deadoralive(life);
                            }

                        }else if(answer2=='N'|answer2=='n'){
                        Console.WriteLine("你決定打道回府......");
                        Console.ReadKey();
                        location=0;
                        }else{
                            Console.WriteLine("請輸入正確的回答!");
                            goto theend;
                        }


                    }
                    hunger=hunger-10;
                    hugercounter(ref hunger,ref life);
                    break;
                    //////////////////////////////////////////////////////////////////////河岸
                    case 4:                         
                    for(int j=0;j<package.Length;j++){              //是否可以釣魚判定
                        if(package[j].item_codename=='竿'){
                            fishing=true;
                        }
                    }
                    if(Task[2].task_complete==false){                                           //尋找水源任務
                        Console.WriteLine("完成任務「尋找水源」，獎勵...水果x3,石頭x2");
                            find_thing='果';
                            store(ref find_thing,ref package);
                            store(ref find_thing,ref package);
                            store(ref find_thing,ref package);
                            find_thing='石';
                            store(ref find_thing,ref package);
                            store(ref find_thing,ref package);
                            Task[2].task_complete=true;
                    }
                   if(fishing==false){                              //釣魚竿有無
                   for(int i=0;i<5;i++){
                    int dice_1=rand.Next()%100;
                    if(dice_1>=0&dice_1<40){
                        find_thing='葉';                 
                        Console.WriteLine("\n找到棕梠葉x1");
                        Console.ReadKey();  
                        store(ref find_thing,ref package);
                        }else if(dice_1>=40&dice_1<80){
                        find_thing='石';      
                        Console.WriteLine("\n找到石頭x1");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        }else if(dice_1>=80&dice_1<100){
                        find_thing='果';      
                        Console.WriteLine("\n找到水果x1");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        }
                     }
                    }else{
                    for(int i=0;i<5;i++){
                    int dice_1=rand.Next()%100;
                    if(dice_1>=0&dice_1<30){
                        find_thing='葉';                 
                        Console.WriteLine("\n找到棕梠葉x1");
                        Console.ReadKey();  
                        store(ref find_thing,ref package);
                        }else if(dice_1>=30&dice_1<60){
                        find_thing='石';      
                        Console.WriteLine("\n找到石頭x1");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        }else if(dice_1>=60&dice_1<80){
                        find_thing='果';      
                        Console.WriteLine("\n找到水果x1");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        }else if(dice_1>=80&dice_1<100){
                        find_thing='肉';      
                        Console.WriteLine("\n釣到魚了，肉塊x2");
                        Console.ReadKey();                                      
                        store(ref find_thing,ref package);
                        store(ref find_thing,ref package);
                        }
                     }
                    }
                    
                    hunger=hunger-10;
                    hugercounter(ref hunger,ref life);
                    break;
                }



        }
    /*////建造函式
            1.秀出建造清單
            2.判斷是否可建造選擇物件
            3.扣除並新增背包物件
            4.判斷任務達成

    */
    static void build(ref int location,ref item[] package,ref int life,ref int hunger,ref int time,ref int day,ref task [] Task){
        int wannabuild;
        int build_thing; 
        Console.WriteLine("建造清單:");
        Console.WriteLine("1.營火\t木頭x5,石頭x2\t");
        Console.WriteLine("2.繩子\t棕梠葉x2\t");
        Console.WriteLine("3.狼煙\t木頭x10\t");
        Console.WriteLine("4.陷阱\t繩子x2,木頭x2，水果x1\t");
        Console.WriteLine("5.魚竿\t木頭x3,繩子x2\t");
        Console.WriteLine("6.長矛\t木頭x2,石頭x2,繩子x1\t");
        Console.WriteLine("7.帆\t皮x6，繩子x2\t");
        Console.WriteLine("8.槳\t木頭x3\t");
        Console.WriteLine("9.船體\t木頭x10\t");
        Console.WriteLine("10.木筏\t帆x1，槳x1，船體x1\t");
        Console.WriteLine("要製作什麼呢?輸入編號...");
        wannabuild=Convert.ToInt32(Console.ReadLine());
        Console.ForegroundColor=ConsoleColor.Red;
        switch(wannabuild){
            case 1:                                                                 //營火
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='木'){
                        if(package[k].item_num>=5){
                            for(int j=0;j<package.Length;j++){
                                if(package[j].item_codename=='石'){
                                    if(package[j].item_num>=2){                                        
                                        Console.WriteLine("\n成功建造營火");
                                        build_thing='火';                 
                                        Console.ReadKey();  
                                        store(ref build_thing,ref package);
                                        package[k].item_num=package[k].item_num-5;   //扣除材料
                                        package[j].item_num=package[j].item_num-2;
                                        hunger=hunger-10;
                                        hugercounter(ref hunger,ref life);
                                        Console.ResetColor();
                                        return;
                                    }

                                }

                            }
                        }

                    }

                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();

            break;
            case 2:                                                                 //繩子
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='葉'){
                        if(package[k].item_num>=2){                            
                                        Console.WriteLine("\n成功建造繩子");
                                        build_thing='繩';                 
                                        Console.ReadKey();  
                                        store(ref build_thing,ref package);
                                        package[k].item_num=package[k].item_num-2;
                                        hunger=hunger-10;
                                        hugercounter(ref hunger,ref life);
                                        Console.ResetColor();
                                        return;                     
                                                           
                        }
                    }
                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();
            break;
            case 3:                                                                 //狼煙
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='木'){
                        if(package[k].item_num>=10){                                                         
                                        Console.WriteLine("\n成功建造狼煙");
                                        build_thing='煙';                 
                                        Console.ReadKey();  
                                        store(ref build_thing,ref package);
                                        package[k].item_num=package[k].item_num-10;
                                        Task[3].task_complete=true;
                                        Task[3].task_form="......已完成";
                                        hunger=hunger-10;
                                        hugercounter(ref hunger,ref life);
                                        Console.ResetColor();
                                        return;                                 
                        }

                    }

                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();
            break;
            case 4:                                                                 //陷阱
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='繩'){
                        if(package[k].item_num>=2){
                            for(int j=0;j<package.Length;j++){
                                if(package[j].item_codename=='木'){
                                    if(package[j].item_num>=2){
                                        for(int i=0;i<package.Length;i++){
                                            if(package[i].item_codename=='果'){
                                                if(package[i].item_num>=1){
                                                        Console.WriteLine("\n成功建造陷阱");
                                                        build_thing='阱';                 
                                                        Console.ReadKey();  
                                                        store(ref build_thing,ref package);
                                                        package[k].item_num=package[k].item_num-2;
                                                        package[j].item_num=package[j].item_num-2;
                                                        package[i].item_num=package[i].item_num-1;
                                                        hunger=hunger-10;
                                                        hugercounter(ref hunger,ref life);
                                                        Console.ResetColor();
                                                        return;                                                                 
                                                }                                  
                                            }
                                        }                                                       
                                    }                                  
                                }
                            }                                                                      
                        }
                    }
                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();
            break;        
            case 5:                                                                 //魚竿
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='木'){
                        if(package[k].item_num>=3){
                            for(int j=0;j<package.Length;j++){
                                if(package[j].item_codename=='繩'){
                                    if(package[j].item_num>=2){                                        
                                        Console.WriteLine("\n成功建造魚竿");
                                        build_thing='竿';                 
                                        Console.ReadKey();  
                                        store(ref build_thing,ref package);
                                        package[k].item_num=package[k].item_num-3;   //扣除材料
                                        package[j].item_num=package[j].item_num-2;
                                        hunger=hunger-10;
                                        hugercounter(ref hunger,ref life);
                                        Console.ResetColor();
                                        return;
                                    }

                                }

                            }
                        }

                    }

                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();
            break;             
            case 6:                                                                 //長矛
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='木'){
                        if(package[k].item_num>=2){
                            for(int j=0;j<package.Length;j++){
                                if(package[j].item_codename=='石'){
                                    if(package[j].item_num>=2){
                                        for(int i=0;i<package.Length;i++){
                                            if(package[i].item_codename=='繩'){
                                                if(package[i].item_num>=1){
                                                        Console.WriteLine("\n成功建造長矛");
                                                        build_thing='矛';                 
                                                        Console.ReadKey();  
                                                        store(ref build_thing,ref package);
                                                        package[k].item_num=package[k].item_num-2;
                                                        package[j].item_num=package[j].item_num-2;
                                                        package[i].item_num=package[i].item_num-1;
                                                        hunger=hunger-10;
                                                        hugercounter(ref hunger,ref life);
                                                        Console.ResetColor();
                                                        return;                                                                 
                                                }                                  
                                            }
                                        }                                                       
                                    }                                  
                                }
                            }                                                                      
                        }
                    }
                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();
            break; 
            case 7:                                                                 //船帆
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='皮'){
                        if(package[k].item_num>=6){
                            for(int j=0;j<package.Length;j++){
                                if(package[j].item_codename=='繩'){
                                    if(package[j].item_num>=2){                                        
                                        Console.WriteLine("\n成功建造船帆");
                                        build_thing='帆';                 
                                        Console.ReadKey();  
                                        store(ref build_thing,ref package);
                                        package[k].item_num=package[k].item_num-6;   //扣除材料
                                        package[j].item_num=package[j].item_num-2;
                                        hunger=hunger-10;
                                        hugercounter(ref hunger,ref life);
                                        Console.ResetColor();
                                        return;
                                    }

                                }

                            }
                        }

                    }

                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();
            break;    
            case 8:                                                                 //船槳
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='木'){
                        if(package[k].item_num>=3){                            
                                        Console.WriteLine("\n成功建造船槳");
                                        build_thing='槳';                 
                                        Console.ReadKey();  
                                        store(ref build_thing,ref package);
                                        package[k].item_num=package[k].item_num-3;
                                        hunger=hunger-10;
                                        hugercounter(ref hunger,ref life);
                                        Console.ResetColor();
                                        return;                     
                                                           
                        }
                    }
                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();
            break;
            case 9:                                                                 //船板
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='木'){
                        if(package[k].item_num>=10){                            
                                        Console.WriteLine("\n成功建造船板");
                                        build_thing='板';                 
                                        Console.ReadKey();  
                                        store(ref build_thing,ref package);
                                        package[k].item_num=package[k].item_num-10;
                                        hunger=hunger-10;
                                        hugercounter(ref hunger,ref life);
                                        Console.ResetColor();
                                        return;                     
                                                           
                        }
                    }
                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();
            break;
            case 10:                                                                 //逃生船
                for(int k=0;k<package.Length;k++){
                    if(package[k].item_codename=='帆'){
                        if(package[k].item_num>=1){
                            for(int j=0;j<package.Length;j++){
                                if(package[j].item_codename=='槳'){
                                    if(package[j].item_num>=1){
                                        for(int i=0;i<package.Length;i++){
                                            if(package[i].item_codename=='板'){
                                                if(package[i].item_num>=1){
                                                        Console.WriteLine("\n成功建造逃生船");
                                                        build_thing='船';                 
                                                        Console.ReadKey();  
                                                        store(ref build_thing,ref package);
                                                        Task[4].task_activate= true;
                                                        Console.WriteLine("你有新任務");
                                                        package[k].item_num=package[k].item_num-1;
                                                        package[j].item_num=package[j].item_num-1;
                                                        package[i].item_num=package[i].item_num-1;
                                                        hunger=hunger-10;
                                                        hugercounter(ref hunger,ref life);
                                                        Console.ResetColor();
                                                        return;                                                                 
                                                }                                  
                                            }
                                        }                                                       
                                    }                                  
                                }
                            }                                                                      
                        }
                    }
                }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("材料不足");
                Console.ReadKey();
            break;                                      
        }
    }
    /////////////////
    /////////////////
    /////////////////
    ////////////結局 救援成功判定
    static void end02( ref int location,ref item[] package,ref int life,ref int hunger,ref int time,ref int day,ref task [] Task ){
            if(day==11){
                Console.Clear();
                Console.ForegroundColor=ConsoleColor.Blue;   
                if (Task[03].task_complete){
                    Console.WriteLine("經歷了一個多禮拜驚險的求生，路過的救援直升機恰巧看到你升起的狼煙，你幸運地獲救，並將這次旅程做成了遊戲");
                    Console.ReadKey();
                    Console.ForegroundColor=ConsoleColor.Red;
                    Console.WriteLine("達成結局「救援成功」");
                    Console.ReadKey();
                            Console.WriteLine("恭喜逃出無人島!");
                            Console.ReadKey();
                            Console.WriteLine("Thanks fo playing....");
                            Console.ReadKey();
                            Console.WriteLine("作者：林正翌");
                            Console.ReadKey();
                            Console.WriteLine("按下任何鍵結束遊戲...");
                            Console.ReadKey();
                            System.Environment.Exit(0);
                }else
                {
                    Console.WriteLine("經過了10天的生存，你的身體越來越虛弱，在苦無救援的情況下你慢慢的倒下了......");
                    Console.ReadKey();
                    Console.ForegroundColor=ConsoleColor.Red;
                    Console.WriteLine("遊戲結束");
                    Console.ReadKey();
                    System.Environment.Exit(0);
                }

            }

    } 
      
    }
}

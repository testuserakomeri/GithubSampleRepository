using System;
using System.Collections.Generic;

namespace GitHubSampleApplication
{
    // =================================================================
    // GitHub ブランチ管理・一人二役検証用サンプルプログラム
    // =================================================================
    // 【シナリオ例】
    // 1. mainブランチ：このベースコードが配置されている
    // 2. アカウントA（作業者）：新機能（例: タスクの削除機能）を追加するブランチを作成
    // 3. アカウントB（レビュアー）：Pull Requestを確認し、コードレビューをしてmainへマージ
    // =================================================================

    class Program
    {
        private static List<TaskItem> _tasks = new List<TaskItem>();

        static void Main(string[] args)
        {
            // 初期データの投入
            InitializeDummyData();

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static void InitializeDummyData()
        {
            _tasks.Add(new TaskItem(1, "GitHub 説明資料の作成", "一人2アカウント運用の図解を含める", DateTime.Now.AddDays(2)));
            _tasks.Add(new TaskItem(2, "C# サンプルファイルのアップロード", "GitHubのWeb画面から直接配置する", DateTime.Now.AddDays(0)));
            _tasks.Add(new TaskItem(3, "ブランチ運用のシミュレーション", "アカウントAとBを切り替えてPRを作成・マージする", DateTime.Now.AddDays(5)));
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("        GitHub 検証用 タスク管理システム (v1.0)   ");
            Console.WriteLine("==================================================");
            Console.WriteLine("1) [VEIW] タスク一覧を表示");
            Console.WriteLine("2) [ADD] 新規タスクを追加");
            Console.WriteLine("3) 選択したタスクを完了状態に更新します");
            Console.WriteLine("4) システムを安全に終了します");
            Console.WriteLine("==================================================");
            Console.Write("メニュー番号を選択してください: ");

            switch (Console.ReadLine())
            {
                case "1":
                    DisplayTasks();
                    return true;
                case "2":
                    AddTask();
                    return true;
                case "3":
                    CompleteTask();
                    return true;
                case "4":
                    return false;
                default:
                    Console.WriteLine("\n無効な入力です。エンターキーを押してやり直してください。");
                    Console.ReadLine();
                    return true;
            }
        }

        private static void DisplayTasks()
        {
            Console.Clear();
            Console.WriteLine("--- タスク一覧 ---");
            if (_tasks.Count == 0)
            {
                Console.WriteLine("タスクはありません。");
            }
            else
            {
                foreach (var task in _tasks)
                {
                    Console.WriteLine(task.ToString());
                }
            }
            Console.WriteLine("\nエンターキーを押すとメニューに戻ります。");
            Console.ReadLine();
        }

        private static void AddTask()
        {
            Console.Clear();
            Console.WriteLine("--- タスクの新規追加 ---");

            Console.Write("タイトル: タスク管理のタイトルを追加します。");
            string title = Console.ReadLine();

            Console.Write("詳細説明: タスク管理の詳細説明を追加します。");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("タイトルは必須です。追加に失敗しました。");
                Console.ReadLine();
                return;
            }

            int newId = _tasks.Count + 1;
            _tasks.Add(new TaskItem(newId, title, description, DateTime.Now.AddDays(3)));

            Console.WriteLine($"\nタスク「{title}」を追加しました！");
            Console.WriteLine("エンターキーを押すとメニューに戻ります。");
            Console.ReadLine();
        }

        private static void CompleteTask()
        {
            Console.Clear();
            Console.WriteLine("--- タスクの完了処理 ---");

            foreach (var task in _tasks)
            {
                if (!task.IsCompleted)
                {
                    Console.WriteLine($"ID: {task.Id} | Title: {task.Title}");
                }
            }

            Console.Write("\n完了にするタスクのIDを入力してください: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var task = _tasks.Find(t => t.Id == id);
                if (task != null)
                {
                    task.IsCompleted = true;
                    Console.WriteLine($"\nタスク「{task.Title}」を完了にしました！");
                }
                else
                {
                    Console.WriteLine("\n指定されたIDのタスクが見つかりません。");
                }
            }
            else
            {
                Console.WriteLine("\n不正な入力です。");
            }

            Console.WriteLine("エンターキーを押すとメニューに戻ります。");
            Console.ReadLine();
        }
    }

    class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem(int id, string title, string description, DateTime dueDate)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;
        }

        public override string ToString()
        {
            string status = IsCompleted ? "[完了]" : "[未完了]";
            return $"{status} ID: {Id} | タイトル: {Title}\n       説明: {Description}\n       期限: {DueDate.ToString("yyyy/MM/dd")}\n--------------------------------------------------";
        }
    }
}

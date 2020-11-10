﻿namespace Client
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.enterChat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nicknameData = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.userList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.messageData = new System.Windows.Forms.TextBox();
            this.userMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nameData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // enterChat
            // 
            this.enterChat.Enabled = false;
            this.enterChat.Location = new System.Drawing.Point(438, 4);
            this.enterChat.Name = "enterChat";
            this.enterChat.Size = new System.Drawing.Size(107, 23);
            this.enterChat.TabIndex = 0;
            this.enterChat.Text = "Подключится";
            this.enterChat.UseVisualStyleBackColor = true;
            this.enterChat.Click += new System.EventHandler(this.enterChat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Для подключении к чату введите свой ник: ";
            // 
            // nicknameData
            // 
            this.nicknameData.Enabled = false;
            this.nicknameData.Location = new System.Drawing.Point(230, 5);
            this.nicknameData.Name = "nicknameData";
            this.nicknameData.Size = new System.Drawing.Size(206, 20);
            this.nicknameData.TabIndex = 2;
            // 
            // chatBox
            // 
            this.chatBox.Enabled = false;
            this.chatBox.Location = new System.Drawing.Point(7, 53);
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.Size = new System.Drawing.Size(376, 291);
            this.chatBox.TabIndex = 3;
            this.chatBox.Text = "";
            // 
            // userList
            // 
            this.userList.Enabled = false;
            this.userList.FormattingEnabled = true;
            this.userList.Location = new System.Drawing.Point(389, 53);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(156, 316);
            this.userList.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Чат: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(392, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Список пользователей: ";
            // 
            // messageData
            // 
            this.messageData.Enabled = false;
            this.messageData.Location = new System.Drawing.Point(7, 350);
            this.messageData.Name = "messageData";
            this.messageData.Size = new System.Drawing.Size(376, 20);
            this.messageData.TabIndex = 7;
            this.messageData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.messageData_KeyUp);
            // 
            // userMenu
            // 
            this.userMenu.Name = "userMenu";
            this.userMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // nameData
            // 
            this.nameData.AutoSize = true;
            this.nameData.Location = new System.Drawing.Point(44, 25);
            this.nameData.Name = "nameData";
            this.nameData.Size = new System.Drawing.Size(0, 13);
            this.nameData.TabIndex = 9;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 410);
            this.Controls.Add(this.nameData);
            this.Controls.Add(this.messageData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userList);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.nicknameData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.enterChat);
            this.Name = "ChatForm";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enterChat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nicknameData;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.ListBox userList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox messageData;
        private System.Windows.Forms.ContextMenuStrip userMenu;
        private System.Windows.Forms.Label nameData;
    }
}


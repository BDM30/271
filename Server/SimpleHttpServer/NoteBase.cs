﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Collections;

namespace myServer
{
    class NoteBase
    {
        private int amountNotifications;
        public Hashtable allNotification = new Hashtable();
        public NoteBase() {
            amountNotifications = 0;
        }
        public bool addNote(string log, string in_name, double in_x, double in_y)
        {
            List<Notification> notes = new List<Notification>();
            Notification note = new Notification(in_name, log, in_x, in_y, amountNotifications++);
            notes.Add(note);
            allNotification.Add(log, notes);
            printNotes();
            return true;
        }
        public string getNotes(string log)
        {
            string answer = "xx";
            List<Notification> notes = (List<Notification>)allNotification[log];
            foreach (Notification one in notes)
            {
                answer = one.ToString();
            }
            //printNotes();
            return answer;
        }
        public void printNotes()
        {
            ICollection valueCollection = allNotification.Values;
            foreach (List<Notification> one in valueCollection)
            {
                Console.WriteLine(one[0].ToString());
            }
        }
    }
}

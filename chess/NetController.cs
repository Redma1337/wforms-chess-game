using System;
using WebSocketSharp;
using System.Collections.Generic;
using System.Windows.Forms;
using Chess_Game_WForms.chess.objects;
using Chess_Game_WForms.chess.objects.pieces;
using System.Drawing;

namespace Chess_Game_WForms.chess
{
    class NetController
    {
        public string Endpoint { get; set; }

        public NetController()
        {
            Endpoint = "ws://materialistic-resonant-fluorine.glitch.me";
        }

        public void tryConnect()
        {
            using (var ws = new WebSocket(Endpoint))
            {
                ws.OnOpen += onChannelOpen;
                ws.OnMessage += onMessageReceived;
                ws.Connect();
            }
        }

        public void onChannelOpen(object sender, EventArgs e)
        {
            MessageBox.Show("Established connection with the Webserver!");
        }   
        
        public void onMessageReceived(object sender, MessageEventArgs e)
        {
            MessageBox.Show("Received following data from the Server");
        }
    }

}

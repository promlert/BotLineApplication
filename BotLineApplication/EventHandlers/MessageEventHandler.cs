// Copyright 2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using System.Threading.Tasks;
using Line;
using BotLineApplication.Configuration;
using BotLineApplication.Repositories;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;
using BotLineApplication.Models;
using System.Linq;

namespace BotLineApplication.EventHandlers
{
    public class MessageEventHandler : ILineEventHandler
    {
        private string[] message_filters = { "เลขทะเบียนรถ", "รหัสเข้าระบบ" };
        private readonly LineBotSampleConfiguration configuration;
        private readonly ILineDBRepository _lineDB;
        public LineEventType EventType
            => LineEventType.Message;

        public MessageEventHandler(LineBotSampleConfiguration configuration, ILineDBRepository lineDB)
        {
            this.configuration = configuration;
            this._lineDB = lineDB;
        }

        public async Task Handle(ILineBot lineBot, ILineEvent evt)
        {
            var reg = new Regex(@"เลขทะเบียนรถ");
            if (string.IsNullOrEmpty(evt.Message.Text))
                return;

            // The Webhook URL verification uses these invalid token.
            if (evt.ReplyToken == "00000000000000000000000000000000" || evt.ReplyToken == "ffffffffffffffffffffffffffffffff")
                return;

            if (evt.Message.Text.ToLowerInvariant().Contains("ping"))
            {
                var response = new TextMessage($"pong");

                await lineBot.Reply(evt.ReplyToken, response);
            }


            if(evt.Message.MessageType == MessageType.Text && evt.Source.User != null)
            {
                try
                {

                    var message = await _lineDB.GetLogByUserId(evt.Source.User.Id);
                    if (message != null && message.Count() > 0)
                    {
                        var m = message.Where(c => c.Text.Contains(message_filters[0]));
                        if (m.Count() > 0)
                        {
                            var u = await _lineDB.GetByUserName(evt.Source.User.Id);
                            u.VehicleRegistration = evt.Message.Text;
                            await _lineDB.Update(u);
                           
                            var response = new TextMessage($"กรุณาใส่ รหัสเข้าระบบ");
                            await _lineDB.Create(new LogMessage { UserId = evt.Source.User.Id, Text = response.Text, CreateDate = DateTime.Now });
                            await lineBot.Reply(evt.ReplyToken, response);
                        }

                        var m2 = message.Where(c => c.Text.Contains(message_filters[1]));
                        if (m2.Count() > 0)
                        {
                            var u = await _lineDB.GetByUserName(evt.Source.User.Id);
                            u.Account = evt.Message.Text;
                            await _lineDB.Update(u);
                            var response = new TextMessage($"การลงทะเบียนสำเร็จ");
                            await _lineDB.Create(new LogMessage { UserId = evt.Source.User.Id, Text = response.Text, CreateDate = DateTime.Now });
                            await lineBot.Reply(evt.ReplyToken, response);
                        }
                    }
                }
                catch (Exception ex)
                {

                   
                }
            }

            //else if (evt.Message.Text.Contains("เลขทะเบียนรถ"))
            //{
            //    var userName = evt.Source.User.Id;
            //    try
            //    {

            //        var user = await lineBot.GetProfile(evt.Source.User);
            //        userName = $"{user.DisplayName} ({user.UserId})";
            //        var u = await _lineDB.GetByUserName(user.UserId);
            //        if (u == null)
            //        {
            //            int c = await _lineDB.Create(new Models.SourceState { CreateDate = DateTime.Now, DisplayName = user.DisplayName, UserName = user.UserId, GroupName = "", Room = "", SourceType = evt.Source.SourceType.ToString() });
            //        }
            //        else
            //        {
            //            if (u.DisplayName != user.DisplayName)
            //            {

            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }

            //    //  var response = new TextMessage($"You are: {userName}");
            //    var response = new TextMessage($"กรุณาใส่ รหัสเข้าระบบ");
            //    await lineBot.Reply(evt.ReplyToken, response);
            //}
            //else if ( evt.Message.Text.Contains("รหัสเข้าระบบ"))
            //{
            //    var userName = evt.Source.User.Id;
            //    try
            //    {

            //        var user = await lineBot.GetProfile(evt.Source.User);
            //        userName = $"{user.DisplayName} ({user.UserId})";
            //        var u = await _lineDB.GetByUserName(user.UserId);
            //        if (u == null)
            //        {
            //            int c = await _lineDB.Create(new Models.SourceState { CreateDate = DateTime.Now, DisplayName = user.DisplayName, UserName = user.UserId, GroupName = "", Room = "", SourceType = evt.Source.SourceType.ToString() });
            //        }
            //        else
            //        {
            //            if (u.DisplayName != user.DisplayName)
            //            {

            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }

            //    //  var response = new TextMessage($"You are: {userName}");
            //    var response = new TextMessage($"การลงทะเบียนสำเร็จ");
            //    await lineBot.Reply(evt.ReplyToken, response);
            //}
            //else if (evt.Message.Text.Contains("เลขทะเบียนรถ") || evt.Message.Text.Contains("รหัสเข้าระบบ") ) 
            //{
            //    var userName = evt.Source.User.Id;
            //    try
            //    {
           
            //        var user = await lineBot.GetProfile(evt.Source.User);
            //        userName = $"{user.DisplayName} ({user.UserId})";
            //       var u = await _lineDB.GetByUserName(user.UserId);
            //        if (u == null)
            //        {
            //            int c = await _lineDB.Create(new Models.SourceState { CreateDate = DateTime.Now, DisplayName = user.DisplayName, UserName = user.UserId, GroupName = "", Room = "", SourceType = evt.Source.SourceType.ToString() });
            //        }
            //        else
            //        {
            //            if( u.DisplayName != user.DisplayName)
            //            {

            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }

            //  //  var response = new TextMessage($"You are: {userName}");
            //    var response = new TextMessage($"การลงทะเบียนสำเร็จ");
            //    await lineBot.Reply(evt.ReplyToken, response);
            //}
            else if (evt.Message.Text.ToLowerInvariant().Contains("logo"))// read file resource
            {
                var logoUrl = this.configuration.ResourcesUrl + "/Images/Line.Bot.SDK.png";

                Console.WriteLine(logoUrl);
                var response = new ImageMessage(logoUrl, logoUrl);

                await lineBot.Reply(evt.ReplyToken, response);
            }
        }
    }
}

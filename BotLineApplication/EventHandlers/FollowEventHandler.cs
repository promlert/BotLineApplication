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

using System.Threading.Tasks;
using BotLineApplication.Models;
using BotLineApplication.Repositories;
using Line;

namespace BotLineApplication.EventHandlers
{
    public class FollowEventHandler : ILineEventHandler
    {
        private readonly ILineDBRepository _lineDB;
        public LineEventType EventType
            => LineEventType.Follow;
        public FollowEventHandler(ILineDBRepository lineDB)
        {
            _lineDB = lineDB;
        }
        public async Task Handle(ILineBot lineBot, ILineEvent evt)
        {
            string userName = "[UNKNOW USER]";
            string? UserId = null;
            string DisplayName = "";
            try
            {
                var user = await lineBot.GetProfile(evt.Source.User);
                //   userName = $"{user.DisplayName} ({user.UserId})";
                DisplayName = userName = $"{user.DisplayName}";
                UserId = user.UserId;
            }
            catch (LineBotException)
            {
            }
            try
            {
                string msg = $"Welcome, {userName} !\n กรุณาใส่ เลขทะเบียนรถ";
                var response = new TextMessage(msg);
                if (UserId != null)
                {
                    var m = await _lineDB.GetByUserName(UserId);
                    if (m == null)
                    {
                        await _lineDB.Create(new SourceState { CreateDate = DateTime.Now, DisplayName = DisplayName, UserName = UserId, GroupName = "", Room = "", SourceType = evt.Source.SourceType.ToString() });

                    }
                    else
                    {
                        m.DisplayName = DisplayName;
                        m.CreateDate = DateTime.Now;
                        await _lineDB.Update(m);
                    }
                    await _lineDB.Create(new LogMessage { UserId = UserId, Text = msg, CreateDate = DateTime.Now });
                }
                await lineBot.Reply(evt.ReplyToken, response);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

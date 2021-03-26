using System.Collections.Generic;
using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace alice_bot_cs.Template
{
    /*
     * 这是一个Module(Plugin 插件)模版，来源MiraiCS的实现。以下是一个群消息处理的示例。请在复制模版代码后删除新插件中注释，变更类名和构造方法名，且不要删除原来的Template。
     * 目前可实现的接口参考下方
     * https://github.com/Executor-Cheng/Mirai-CSharp/tree/master/Mirai-CSharp/Plugin/Interfaces
     */
    public partial class ModuleTemplate : IGroupMessage // 填写接口
    {
        public ModuleTemplate() // 构造方法，可以保持空
        {
        }

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e) 
        {
            IMessageBase plain1 = new PlainMessage($"收到了来自{e.Sender.Name}[{e.Sender.Id}]{{{e.Sender.Permission}}}的群消息:{string.Join(null, (IEnumerable<IMessageBase>)e.Chain)}"); 
            IMessageBase plain2 = new PlainMessage("QAQ");
            await session.SendGroupMessageAsync(e.Sender.Group.Id, plain1/*, plain2, /* etc... */); // 向消息来源群异步发送由以上chain表示的消息
            return false; // 消息阻隔
        }
    }
}

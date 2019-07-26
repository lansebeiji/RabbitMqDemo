using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GD.CoursePreparationSystem.Domain.Model
{
    /// <summary>
    /// 消息类型（约定：每个类型 对应 QueueName的名字）
    /// </summary>
    public enum MessageType
    {
        ERP_BaseOrg = 0,
        ERP_Warehouse = 1,
        ERP_Partner = 2,

        ERP_AdminUser = 3,
        ERP_AdminUserAuth = 4,
        ERP_BizUser = 5,
        //采购申请二级审核通过
        ERP_SYNC_PurchaseRequsitionAuditedPass = 115,

        //采购申请二级审核通过发送消息给供应链
        ERP_SYNC_SynPurchaseRequisitionItem = 116,

        //采购申请取消或者强制完成发送消息给供应链
        ERP_SYNC_SynPurchaseRequisitionCancle = 117,




    }
}

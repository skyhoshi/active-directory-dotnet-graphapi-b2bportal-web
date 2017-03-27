﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureB2BInvite.Models
{

    public class EmailAddress
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
    }

    public class CcRecipient
    {
        [JsonProperty(PropertyName = "emailAddress")]
        public EmailAddress EmailAddress { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }

        [JsonProperty(PropertyName = "objectId")]
        public string ObjectId { get; set; }

        [JsonProperty(PropertyName = "permissionIdentityType")]
        public string PermissionIdentityType { get; set; }
    }

    public class InvitedUserMessageInfo
    {
        [JsonProperty(PropertyName = "messageLanguage")]
        public string MessageLanguage { get; set; }

        [JsonProperty(PropertyName = "ccRecipients")]
        public List<CcRecipient> CcRecipients { get; set; }

        [JsonProperty(PropertyName = "customizedMessageBody")]
        public string CustomizedMessageBody { get; set; }
    }

    public class InvitedUser
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }

    public class GraphInvitation
    {
        [JsonProperty(PropertyName = "@odata.context") ]
        public string context { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "inviteRedeemUrl")]
        public string InviteRedeemUrl { get; set; }

        [JsonProperty(PropertyName = "invitedUserDisplayName")]
        public string InvitedUserDisplayName { get; set; }

        [JsonProperty(PropertyName = "invitedUserType")]
        public string InvitedUserType { get; set; }

        [JsonProperty(PropertyName = "invitedUserEmailAddress")]
        public string InvitedUserEmailAddress { get; set; }

        [JsonProperty(PropertyName = "sendInvitationMessage")]
        public bool SendInvitationMessage { get; set; }

        [JsonProperty(PropertyName = "invitedUserMessageInfo")]
        public InvitedUserMessageInfo InvitedUserMessageInfo { get; set; }

        [JsonProperty(PropertyName = "inviteRedirectUrl")]
        public string InviteRedirectUrl { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "invitedUser")]
        public InvitedUser InvitedUser { get; set; }
    }

    //error object
    public class ResponseError
    {
        [JsonProperty(PropertyName = "code")]
        public string Code;

        [JsonProperty(PropertyName = "message")]
        public string Message;

        [JsonProperty(PropertyName = "innerError")]
        public InnerError InnerError;
    }

    public class InnerError
    {
        [JsonProperty(PropertyName = "request-id")]
        public string RequestId;

        [JsonProperty(PropertyName = "data")]
        public string Data;
    }
}
/* Options:
Date: 2017-12-08 14:48:41
Version: 4.58
Tip: To override a DTO option, remove "//" prefix before updating

//GlobalNamespace: 
//MakePartial: True
//MakeVirtual: True
//MakeInternal: False
//MakeDataContractsExtensible: False
//AddReturnMarker: True
//AddDescriptionAsComments: True
//AddDataContractAttributes: False
//AddIndexesToDataMembers: False
//AddGeneratedCodeAttributes: False
//AddResponseStatus: False
//AddImplicitVersion: 
//InitializeCollections: True
//ExportValueTypes: False
//IncludeTypes: 
//ExcludeTypes: 
//AddNamespaces: 
//AddDefaultXmlNamespace: http://schemas.servicestack.net/types
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;
using CSharpMessenger.ServiceStack.Entities;
using CSharpMessenger.ServiceStack.Services;


namespace CSharpMessenger.ServiceStack.Entities
{

    public partial class AdditionalDownloadInformation
    {
        public virtual string Type { get; set; }
        public virtual long FileSize { get; set; }
        public virtual string Extension { get; set; }
    }

    public partial class Attachment
        : AttachmentSummary
    {
        public Attachment()
        {
            Recipients = new List<User>{};
            AdditionalDownloadInformation = new List<AdditionalDownloadInformation>{};
        }

        public virtual List<User> Recipients { get; set; }
        public virtual List<AdditionalDownloadInformation> AdditionalDownloadInformation { get; set; }
    }

    public partial class AttachmentGroup
    {
        ///<summary>
        ///Used to group different attachments in a campaign for tracking purposes.
        ///</summary>
        public virtual Guid AttachmentGroupGuid { get; set; }
        ///<summary>
        ///Only has a value when Campaign is type 'Automatic'. Points to the Template attachment that belongs to the template message (never sent with the campaign, only used as a template).
        ///</summary>
        public virtual Guid? TemplateAttachmentGuid { get; set; }
        ///<summary>
        ///The name of the attachment group, which in most cases would be the file name.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string Name { get; set; }
    }

    public partial class AttachmentOptions
    {
        [DataMember]
        public virtual string FYEOType { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool TrackingEnabled { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool TrackingShared { get; set; }
    }

    public partial class AttachmentSummary
    {
        public AttachmentSummary()
        {
            Extensions = new List<Extension>{};
            Labels = new List<Label>{};
            Shares = new List<Share>{};
        }

        public virtual Guid Guid { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Type { get; set; }
        ///<summary>
        ///In bytes.
        ///</summary>
        public virtual long FileSize { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime UploadDate { get; set; }
        public virtual string UploadedBy { get; set; }
        public virtual int ChunkCount { get; set; }
        public virtual bool CanPreview { get; set; }
        public virtual string PreviewUrl { get; set; }
        public virtual AttachmentOptions AttachmentOptions { get; set; }
        public virtual AttachmentTrackingSummary TrackingSummary { get; set; }
        public virtual DateTime? ESignatureDeadline { get; set; }
        public virtual List<Extension> Extensions { get; set; }
        public virtual bool ESignatureRequired { get; set; }
        public virtual DateTime? DateSigned { get; set; }
        public virtual bool ESignatureDeclined { get; set; }
        public virtual string OriginalFileExtension { get; set; }
        public virtual DateTime? LastArchiveDate { get; set; }
        public virtual List<Label> Labels { get; set; }
        public virtual List<Share> Shares { get; set; }
        public virtual bool HasAccess { get; set; }
        public virtual Guid? AttachmentGroupGuid { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        ///<summary>
        ///The X axis position of the QR Code to be rendered on each page. Only valid for ESignatures.
        ///</summary>
        public virtual int? QrCodeXPosition { get; set; }
        ///<summary>
        ///The Y axis position of the QR Code to be rendered on each page. Only valid for ESignatures.
        ///</summary>
        public virtual int? QrCodeYPosition { get; set; }
    }

    public partial class AttachmentTrackingFilter
    {
        public AttachmentTrackingFilter()
        {
            AttachmentGuids = new List<Guid>{};
            Types = new List<string>{};
            AttachmentGroupGuids = new List<Guid>{};
        }

        ///<summary>
        ///Filter attachment tracking activity for these attachments. Max of 25 guids at a time.
        ///</summary>
        [DataMember]
        public virtual List<Guid> AttachmentGuids { get; set; }

        ///<summary>
        ///Filter by Type of tracking activity.
        ///</summary>
        [DataMember]
        public virtual List<string> Types { get; set; }

        ///<summary>
        ///Filter all activity occuring after this datetime.
        ///</summary>
        [DataMember]
        public virtual DateTime? StartTime { get; set; }

        ///<summary>
        ///Filter all activity occuring before this datetime.
        ///</summary>
        [DataMember]
        public virtual DateTime? EndTime { get; set; }

        ///<summary>
        ///Filter by tracking owner (the user that owns the resource).
        ///</summary>
        [DataMember]
        public virtual Guid? OwnerUserGuid { get; set; }

        ///<summary>
        ///Filter by tracking actor (the user performing the action).
        ///</summary>
        [DataMember]
        public virtual Guid? ActorUserGuid { get; set; }

        ///<summary>
        ///Filter by campaign guid.
        ///</summary>
        [DataMember]
        public virtual Guid? CampaignGuid { get; set; }

        ///<summary>
        ///Only used within a Campaign. Filter attachment tracking activity for these attachment groups.
        ///</summary>
        [DataMember]
        public virtual List<Guid> AttachmentGroupGuids { get; set; }

        ///<summary>
        ///Filter attachment tracking where activity was performed by the uploader.
        ///</summary>
        [DataMember]
        public virtual bool? IgnoreUploader { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public virtual bool? OriginalParticipantsOnly { get; set; }
    }

    public partial class AttachmentTrackingSummary
    {
        [DataMember(IsRequired=true)]
        public virtual int TotalSeen { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalRecipients { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalESignaturesRequired { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalDownloaded { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalReviewed { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPreviewed { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPrinted { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalEsigned { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalEsignDeclined { get; set; }
    }

    public partial class BoolFeature
    {
        public virtual bool Enabled { get; set; }
    }

    public partial class Branding
    {
        public virtual string ImageLinkBanner { get; set; }
        public virtual string ImageLink24x { get; set; }
        public virtual string ImageLink48x { get; set; }
        public virtual string ImageLink64x { get; set; }
        public virtual string ImageLinkFavIcon { get; set; }
        public virtual PoweredBy PoweredBy { get; set; }
    }

    public partial class Campaign
    {
        public Campaign()
        {
            TemplateAttachmentGroups = new List<AttachmentGroup>{};
        }

        public virtual Guid Guid { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Mode { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string Status { get; set; }
        ///<summary>
        ///The Message that will be used as a template for this campaign. This will only have a value if the campaign is in Automatic mode.
        ///</summary>
        public virtual Guid? TemplateMessageGuid { get; set; }
        ///<summary>
        ///The Attachments that will be linked to the Message for this campaign. This is only valid if the campaign is in Automatic mode.
        ///</summary>
        public virtual List<AttachmentGroup> TemplateAttachmentGroups { get; set; }
    }

    public partial class CampaignAttachmentTrackingSummary
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGroupGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalDownloaded { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalReviewed { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPreviewed { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPrinted { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalEsigned { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalEsignDeclined { get; set; }
    }

    public partial class CampaignMessageTrackingSummary
        : MessageTrackingSummary
    {
        [DataMember]
        public virtual int TotalSent { get; set; }
    }

    public partial class CampaignRecipient
    {
        [DataMember(IsRequired=true)]
        public virtual string Email { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Status { get; set; }

        [DataMember]
        public virtual Guid? MessageGuid { get; set; }
    }

    public partial class CampaignReportSummary
    {
        [DataMember(IsRequired=true)]
        public virtual Guid Guid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Name { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Status { get; set; }

        [DataMember]
        public virtual Guid? ReportAttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual DateTime StartTime { get; set; }

        [DataMember]
        public virtual DateTime? EndTime { get; set; }
    }

    public partial class Compose
    {
        public virtual bool Enabled { get; set; }
        public virtual string Restrictions { get; set; }
        public virtual string AllowedEmailAddress { get; set; }
    }

    public partial class Contact
    {
        public Contact()
        {
            EmailAliases = new List<EmailAlias>{};
        }

        public virtual Guid UserServiceGuid { get; set; }
        public virtual string State { get; set; }
        public virtual Guid UserGroupGuid { get; set; }
        public virtual string UserGroupName { get; set; }
        public virtual string UserGroupType { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual List<EmailAlias> EmailAliases { get; set; }
    }

    public partial class CRA
    {
        public virtual bool Enabled { get; set; }
        public virtual string CRACaption { get; set; }
        public virtual string SenderHelpLink { get; set; }
        public virtual string RecipientHelpLink { get; set; }
    }

    public partial class DefaultSecureSettings
    {
        public DefaultSecureSettings()
        {
            DefaultSecureDomainList = new List<string>{};
        }

        public virtual string DefaultSecure { get; set; }
        public virtual string DefaultSecureListRegex { get; set; }
        public virtual BoolFeature DefaultSecureListAttach { get; set; }
        public virtual BoolFeature DefaultSecureAttachExcludeEmbedImg { get; set; }
        public virtual List<string> DefaultSecureDomainList { get; set; }
    }

    public partial class EmailAlias
    {
        public virtual string EmailAddress { get; set; }
        public virtual string Status { get; set; }
    }

    public partial class Extension
    {
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
        public virtual bool IsPublic { get; set; }
    }

    public partial class Label
    {
        public virtual Guid LabelGuid { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual bool AllowDelete { get; set; }
        public virtual bool AllowRename { get; set; }
        public virtual bool AllowAssetAdd { get; set; }
        public virtual bool AllowAssetRemove { get; set; }
        public virtual bool AllowAssetShare { get; set; }
        public virtual Guid? UserServiceGuid { get; set; }
        public virtual Guid? ParentLabelGuid { get; set; }
        public virtual string Name { get; set; }
        public virtual string SystemType { get; set; }
    }

    public partial class Localization
    {
        public Localization()
        {
            SupportedLanguages = new List<string>{};
        }

        public virtual bool Enabled { get; set; }
        public virtual List<string> SupportedLanguages { get; set; }
    }

    public partial class Menu
    {
        public virtual MenuItem Help { get; set; }
        public virtual MenuItem CustomItem2 { get; set; }
        public virtual MenuItem CustomItem3 { get; set; }
        public virtual MenuItem CustomItem4 { get; set; }
        public virtual MenuItem CustomItem5 { get; set; }
        public virtual MenuItem ContactAdminItem { get; set; }
        public virtual MenuItem PoliciesItem { get; set; }
        public virtual MenuItem AboutItem { get; set; }
    }

    public partial class MenuItem
    {
        public virtual bool Enabled { get; set; }
        public virtual string Label { get; set; }
        public virtual string Link { get; set; }
    }

    public partial class MessageOptions
    {
        [DataMember]
        public virtual bool? AllowForward { get; set; }

        [DataMember]
        public virtual bool? AllowReply { get; set; }

        [DataMember]
        public virtual bool? AllowTracking { get; set; }

        [DataMember]
        public virtual string FYEOType { get; set; }

        [DataMember]
        public virtual bool? ShareTracking { get; set; }
    }

    public partial class MessageOptionsResponse
    {
        [DataMember(IsRequired=true)]
        public virtual bool AllowForward { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool AllowReply { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool AllowTracking { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string FYEOType { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool ShareTracking { get; set; }
    }

    public partial class MessageSummary
    {
        public MessageSummary()
        {
            To = new List<User>{};
            Cc = new List<User>{};
            Bcc = new List<User>{};
            Attachments = new List<AttachmentSummary>{};
            Labels = new List<Label>{};
            Extensions = new List<Extension>{};
            Shares = new List<Share>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid Guid { get; set; }

        [DataMember]
        public virtual Guid? ParentGuid { get; set; }

        [DataMember]
        public virtual Guid? CampaignGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual DateTime Date { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Subject { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Status { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Action { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Method { get; set; }

        [DataMember(IsRequired=true)]
        public virtual User Sender { get; set; }

        [DataMember]
        public virtual string ReplyTo { get; set; }

        [DataMember]
        public virtual List<User> To { get; set; }

        [DataMember]
        public virtual List<User> Cc { get; set; }

        [DataMember]
        public virtual List<User> Bcc { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string BodyFormat { get; set; }

        [DataMember(IsRequired=true)]
        public virtual MessageOptionsResponse MessageOptions { get; set; }

        [DataMember]
        public virtual List<AttachmentSummary> Attachments { get; set; }

        [DataMember]
        public virtual List<Label> Labels { get; set; }

        [DataMember]
        public virtual List<Extension> Extensions { get; set; }

        [DataMember]
        public virtual DateTime? LastArchiveDate { get; set; }

        [DataMember]
        public virtual DateTime? ExpiryDate { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string ExpiryGroup { get; set; }

        [DataMember]
        public virtual DateTime? RetentionDate { get; set; }

        [DataMember]
        public virtual DateTime? RecallDate { get; set; }

        [DataMember]
        public virtual string RecallReason { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool Retrieved { get; set; }

        [DataMember]
        public virtual MessageTrackingSummary TrackingSummary { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid ConversationGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string SenderIpAddress { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<Share> Shares { get; set; }

        public virtual bool HasAccess { get; set; }
    }

    public partial class MessageTrackingFilter
    {
        public MessageTrackingFilter()
        {
            MessageGuids = new List<Guid>{};
            Types = new List<string>{};
        }

        ///<summary>
        ///Filter message tracking activity for these messages. Max of 25 guids at a time.
        ///</summary>
        [DataMember]
        public virtual List<Guid> MessageGuids { get; set; }

        ///<summary>
        ///Filter by Type of tracking activity.
        ///</summary>
        [DataMember]
        public virtual List<string> Types { get; set; }

        ///<summary>
        ///Filter all activity occuring after this datetime.
        ///</summary>
        [DataMember]
        public virtual DateTime? StartTime { get; set; }

        ///<summary>
        ///Filter all activity occuring before this datetime.
        ///</summary>
        [DataMember]
        public virtual DateTime? EndTime { get; set; }

        ///<summary>
        ///Filter by tracking owner (the user that owns the resource).
        ///</summary>
        [DataMember]
        public virtual Guid? OwnerUserGuid { get; set; }

        ///<summary>
        ///Filter by tracking actor (the user performing the action).
        ///</summary>
        [DataMember]
        public virtual Guid? ActorUserGuid { get; set; }

        ///<summary>
        ///Filter by campaign guid.
        ///</summary>
        [DataMember]
        public virtual Guid? CampaignGuid { get; set; }

        ///<summary>
        ///Filter message tracking where activity was performed by the sender.
        ///</summary>
        [DataMember]
        public virtual bool? IgnoreSender { get; set; }
    }

    public partial class MessageTrackingSummary
    {
        [DataMember(IsRequired=true)]
        public virtual int TotalRecipients { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalRetrieved { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalReviewed { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalReplied { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalForwarded { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPrinted { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalDeleted { get; set; }
    }

    public partial class MessageTreeItem
    {
        public MessageTreeItem()
        {
            To = new List<User>{};
            Cc = new List<User>{};
            Bcc = new List<User>{};
            ChildMessages = new List<MessageTreeItem>{};
        }

        public virtual Guid MessageGuid { get; set; }
        public virtual User MessageSender { get; set; }
        public virtual string MessageSubject { get; set; }
        public virtual DateTime? MessageSentOn { get; set; }
        public virtual string MessageStatus { get; set; }
        public virtual string BranchType { get; set; }
        public virtual int RelativeDepth { get; set; }
        public virtual List<User> To { get; set; }
        public virtual List<User> Cc { get; set; }
        public virtual List<User> Bcc { get; set; }
        public virtual List<MessageTreeItem> ChildMessages { get; set; }
    }

    public partial class Notification
    {
        public virtual bool Enabled { get; set; }
    }

    public partial class OutOfOffice
    {
        public virtual bool Enabled { get; set; }
        public virtual string Message { get; set; }
    }

    public partial class PasswordManagement
    {
        public virtual bool Enabled { get; set; }
        public virtual int MinimumPasswordLength { get; set; }
        public virtual int MaximumPasswordLength { get; set; }
        public virtual int MinimumCapitalCharacters { get; set; }
        public virtual int MinimumNumericCharacters { get; set; }
        public virtual int MinimumSymbols { get; set; }
    }

    public partial class PoweredBy
    {
        public virtual bool Enabled { get; set; }
        public virtual string ImageLink { get; set; }
        public virtual string ImageText { get; set; }
        public virtual string WebsiteUrl { get; set; }
    }

    public partial class RestrictedExtensions
    {
        public RestrictedExtensions()
        {
            Extensions = new List<string>{};
        }

        public virtual bool Enabled { get; set; }
        public virtual List<string> Extensions { get; set; }
    }

    public partial class SearchAttachmentFilter
    {
        public SearchAttachmentFilter()
        {
            Associations = new List<string>{};
            AttachmentGuids = new List<Guid>{};
            Types = new List<string>{};
            UploaderGuids = new List<Guid>{};
            RecipientGuids = new List<Guid>{};
            AttachmentGroupGuids = new List<Guid>{};
            LabelGuids = new List<Guid>{};
        }

        [DataMember]
        public virtual List<string> Associations { get; set; }

        ///<summary>
        ///Search by all or part of file name
        ///</summary>
        [DataMember]
        public virtual string SearchCriteria { get; set; }

        ///<summary>
        ///Search by all or part of any extension name
        ///</summary>
        [DataMember]
        public virtual string SearchExtensionNameCriteria { get; set; }

        ///<summary>
        ///Search by all or part of any extension value
        ///</summary>
        [DataMember]
        public virtual string SearchExtensionValueCriteria { get; set; }

        ///<summary>
        ///Max 25.
        ///</summary>
        [DataMember]
        public virtual List<Guid> AttachmentGuids { get; set; }

        [DataMember]
        public virtual List<string> Types { get; set; }

        [DataMember]
        public virtual List<Guid> UploaderGuids { get; set; }

        [DataMember]
        public virtual List<Guid> RecipientGuids { get; set; }

        [DataMember]
        public virtual Guid? CampaignGuid { get; set; }

        [DataMember]
        public virtual List<Guid> AttachmentGroupGuids { get; set; }

        [DataMember]
        public virtual List<Guid> LabelGuids { get; set; }

        ///<summary>
        ///If HashAlgorithm has a value, HashValue must also have a value.
        ///</summary>
        [DataMember]
        public virtual string HashAlgorithm { get; set; }

        ///<summary>
        ///If HashValue has a value, HashAlgorithm must also have a value.
        ///</summary>
        [DataMember]
        public virtual string HashValue { get; set; }
    }

    public partial class SearchAttachmentSort
    {
        [DataMember]
        public virtual string Type { get; set; }

        [DataMember]
        public virtual string Direction { get; set; }
    }

    public partial class SearchMessagesFilter
    {
        public SearchMessagesFilter()
        {
            Types = new List<string>{};
            MessageGuids = new List<Guid>{};
            SenderGuids = new List<Guid>{};
            RecipientGuids = new List<Guid>{};
            LabelGuids = new List<Guid>{};
        }

        ///<summary>
        ///Search the message Subject and associated users (email, first name, last name). Only a partial match is required to return a match.
        ///</summary>
        [DataMember]
        public virtual string SearchCriteria { get; set; }

        ///<summary>
        ///Search message by extension name. Only a partial match is required to return a match.
        ///</summary>
        [DataMember]
        public virtual string SearchExtensionNameCriteria { get; set; }

        ///<summary>
        ///Search message by extension value. Only a partial match is required to return a match.
        ///</summary>
        [DataMember]
        public virtual string SearchExtensionValueCriteria { get; set; }

        ///<summary>
        ///Defaults to search all folders.
        ///</summary>
        [DataMember]
        public virtual List<string> Types { get; set; }

        ///<summary>
        ///Max 25.
        ///</summary>
        [DataMember]
        public virtual List<Guid> MessageGuids { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public virtual Guid? CampaignGuid { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public virtual List<Guid> SenderGuids { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public virtual List<Guid> RecipientGuids { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public virtual List<Guid> LabelGuids { get; set; }
    }

    public partial class SearchMessagesSort
    {
        ///<summary>
        ///Defaults to Date.
        ///</summary>
        [DataMember]
        public virtual string Type { get; set; }

        ///<summary>
        ///Defaults to Desc.
        ///</summary>
        [DataMember]
        public virtual string Direction { get; set; }
    }

    public partial class Share
    {
        [DataMember(IsRequired=true)]
        public virtual Guid Guid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid AssetGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string ShareType { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid UserServiceGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid SharerGuid { get; set; }

        [DataMember]
        public virtual Guid? UnsharerGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool IsActive { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool IsPublic { get; set; }

        [DataMember(IsRequired=true)]
        public virtual DateTime ShareDate { get; set; }

        [DataMember]
        public virtual DateTime? UnshareDate { get; set; }

        [DataMember]
        public virtual string UnshareReason { get; set; }

        [DataMember]
        public virtual bool? IsRecipient { get; set; }

        [DataMember]
        public virtual bool? IsRead { get; set; }
    }

    public partial class Signature
    {
        public Signature()
        {
            Placeholders = new List<SignaturePlaceholder>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid Guid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual SignatureUser User { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual DateTime? SignedDate { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual DateTime? DeclinedDate { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string Answer { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string IpAddress { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual List<SignaturePlaceholder> Placeholders { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public virtual string DeclineReason { get; set; }
    }

    public partial class SignatureDraft
    {
        public SignatureDraft()
        {
            Placeholders = new List<SignatureDraftPlaceholder>{};
        }

        [DataMember(IsRequired=true)]
        public virtual string EmailAddress { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<SignatureDraftPlaceholder> Placeholders { get; set; }
    }

    public partial class SignatureDraftPlaceholder
    {
        [DataMember(IsRequired=true)]
        public virtual string Type { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int Page { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int XPosition { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int YPosition { get; set; }
    }

    public partial class SignaturePlaceholder
    {
        [DataMember]
        public virtual Guid? Guid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual DateTime CreatedDate { get; set; }

        [DataMember]
        public virtual DateTime? SignedDate { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Type { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int Page { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int XPosition { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int YPosition { get; set; }
    }

    public partial class SignatureUser
    {
        [DataMember(IsRequired=true)]
        public virtual Guid UserGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Email { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string FirstName { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string LastName { get; set; }
    }

    public partial class Sso
    {
        public virtual bool Enabled { get; set; }
    }

    public partial class SubscriptionPayload
    {
        public virtual string SubscriptionType { get; set; }
        public virtual Guid SubscriptionGuid { get; set; }
    }

    public partial class Terms
    {
        public virtual bool Enabled { get; set; }
        public virtual string TermsLink { get; set; }
    }

    public partial class TrackingDetail
    {
        public TrackingDetail()
        {
            RelatedMessageRecipients = new List<User>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid OwnerUserGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string OwnerUserEmail { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid ActorUserGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string ActorUserEmail { get; set; }

        [DataMember]
        public virtual Guid? MessageGuid { get; set; }

        [DataMember]
        public virtual Guid? RelatedMessageGuid { get; set; }

        [DataMember]
        public virtual bool? HasAccessToRelatedMessage { get; set; }

        [DataMember]
        public virtual List<User> RelatedMessageRecipients { get; set; }

        [DataMember]
        public virtual Guid? AttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string TrackingType { get; set; }

        [DataMember(IsRequired=true)]
        public virtual DateTime Date { get; set; }

        [DataMember]
        public virtual string ClientName { get; set; }

        [DataMember]
        public virtual string ClientVersion { get; set; }

        [DataMember]
        public virtual string IpAddress { get; set; }

        [DataMember]
        public virtual string MessageSubject { get; set; }

        [DataMember]
        public virtual string AttachmentName { get; set; }
    }

    public partial class TrackingSort
    {
        ///<summary>
        ///Sorts the results by the indicated sort type. Defaults to TrackingDate.
        ///</summary>
        [DataMember]
        public virtual string Type { get; set; }

        ///<summary>
        ///Alters sorting direction of the results. Defaults to Desc.
        ///</summary>
        [DataMember]
        public virtual string Direction { get; set; }
    }

    public partial class UniversalAccess
    {
        public virtual UniversalAccessItem OutlookToolbar { get; set; }
        public virtual UniversalAccessItem ChromeExtension { get; set; }
        public virtual UniversalAccessItem DesktopApp { get; set; }
        public virtual UniversalAccessItem DesktopAppMac { get; set; }
        public virtual UniversalAccessItem iOS { get; set; }
        public virtual UniversalAccessItem Android { get; set; }
        public virtual UniversalAccessItem BlackBerry { get; set; }
        public virtual UniversalAccessItem WindowsPhone8 { get; set; }
        public virtual UniversalAccessItem MacApp { get; set; }
        public virtual UniversalAccessItem Office365 { get; set; }
        public virtual UniversalAccessItem MSDynamicsApp { get; set; }
        public virtual UniversalAccessItem SalesforceApp { get; set; }
        public virtual UniversalAccessItem Quickbooks { get; set; }
    }

    public partial class UniversalAccessItem
    {
        public virtual bool Enabled { get; set; }
        public virtual string DownloadLink { get; set; }
        public virtual string AppId { get; set; }
    }

    public partial class User
    {
        ///<summary>
        ///When creating a message the Guid will have value if user exists in the Portal. However it will be null when a message is still in draft mode.
        ///</summary>
        [DataMember]
        public virtual Guid? Guid { get; set; }

        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }

    public partial class UserGroup
    {
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
    }

    public partial class UserTrackingFilter
    {
        public UserTrackingFilter()
        {
            Types = new List<string>{};
            MessageGuids = new List<Guid>{};
            AttachmentGuids = new List<Guid>{};
        }

        ///<summary>
        ///Filter by Type of tracking activity.
        ///</summary>
        [DataMember]
        public virtual List<string> Types { get; set; }

        ///<summary>
        ///Filter all activity occuring after this datetime.
        ///</summary>
        [DataMember]
        public virtual DateTime? StartTime { get; set; }

        ///<summary>
        ///Filter all activity occuring before this datetime.
        ///</summary>
        [DataMember]
        public virtual DateTime? EndTime { get; set; }

        ///<summary>
        ///Filter by tracking owner (the user that owns the resource).
        ///</summary>
        [DataMember]
        public virtual Guid? OwnerUserGuid { get; set; }

        ///<summary>
        ///Filter by tracking actor (the user performing the action).
        ///</summary>
        [DataMember]
        public virtual Guid? ActorUserGuid { get; set; }

        ///<summary>
        ///Filter User tracking activity where you are the Actor.
        ///</summary>
        [DataMember]
        public virtual bool? IgnoreSelf { get; set; }

        ///<summary>
        ///Filter User tracking activity for these messages. Max of 25 guids at a time.
        ///</summary>
        [DataMember]
        public virtual List<Guid> MessageGuids { get; set; }

        ///<summary>
        ///Filter User tracking activity for these attachments. Max of 25 guids at a time.
        ///</summary>
        [DataMember]
        public virtual List<Guid> AttachmentGuids { get; set; }
    }
}

namespace CSharpMessenger.ServiceStack.Services
{

    [Route("/v1/labels/{LabelGuid}/attachments/{AttachmentGuid}", "POST")]
    [DataContract]
    public partial class AddAttachmentLabel
        : IReturn<AddAttachmentLabelResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid LabelGuid { get; set; }
    }

    [DataContract]
    public partial class AddAttachmentLabelResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/campaigns/{CampaignGuid}/recipients", "POST")]
    [DataContract]
    public partial class AddCampaignRecipients
        : IReturn<AddCampaignRecipientsResponse>
    {
        public AddCampaignRecipients()
        {
            EmailAddresses = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }

        ///<summary>
        ///Maximum of 500 email addresses at a time. Invalid and blacklisted emails are accepted (they are checked when campaign processing begins).
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual List<string> EmailAddresses { get; set; }
    }

    [DataContract]
    public partial class AddCampaignRecipientsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/labels/{LabelGuid}/messages/{MessageGuid}", "POST")]
    [DataContract]
    public partial class AddMessageLabel
        : IReturn<AddMessageLabelResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid LabelGuid { get; set; }
    }

    [DataContract]
    public partial class AddMessageLabelResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/user/aliases", "POST")]
    [Route("/v1/user/aliases", "POST")]
    [DataContract]
    public partial class AddUserAlias
        : IReturn<AddUserAliasResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string EmailAlias { get; set; }
    }

    [DataContract]
    public partial class AddUserAliasResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public partial class AttachmentPlaceholder
    {
        public AttachmentPlaceholder()
        {
            Chunks = new List<AttachmentPlaceholderChunk>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string FileName { get; set; }

        ///<summary>
        ///The placeholders for the Chunks of an Attachment. Use this information to seperate the Attachment into Chunks. Attachments that are smaller than the Chunk Byte Size will only require 1 Chunk. An Attachments Chunk Numbers are in sequence (eg. 1,2,3,4) and always starts at 1.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual List<AttachmentPlaceholderChunk> Chunks { get; set; }
    }

    [DataContract]
    public partial class AttachmentPlaceholderChunk
    {
        ///<summary>
        ///The sequence number of the attachment Chunk.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual int ChunkNumber { get; set; }

        ///<summary>
        ///The size in Bytes of of this Chunk only.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long BytesSize { get; set; }

        ///<summary>
        ///The start byte index that should be used to slice the chunk in relation to the entire Attachments byte stream.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long ByteStartIndex { get; set; }

        ///<summary>
        ///The end byte index that should be used to slice the chunk in relation to the entire Attachments byte stream.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long ByteEndIndex { get; set; }

        ///<summary>
        ///The URI that should be used to POST the chunk to.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string UploadUri { get; set; }
    }

    public partial class AttachmentProgress
    {
        public AttachmentProgress()
        {
            ChunkProgresses = new List<ChunkProgress>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string FileName { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string Status { get; set; }

        ///<summary>
        ///Total expected bytes of the attachment.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long TotalBytes { get; set; }

        ///<summary>
        ///This is calculated from the total number of chunks that have been successfully uploaded. Does not include partially uploaded chunks. 
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long UploadedBytes { get; set; }

        ///<summary>
        ///A list of attachments Chunks statuses.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual List<ChunkProgress> ChunkProgresses { get; set; }
    }

    [Route("/authenticate", "POST")]
    [Route("/v1/authenticate", "POST")]
    [DataContract]
    public partial class Authenticate
        : IReturn<AuthenticateResponse>
    {
        ///<summary>
        ///A new authentication key for a user can be obtained by calling GetNewUserAuthKey
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string AuthenticationToken { get; set; }

        ///<summary>
        ///Determines whether session-token should be set in the Cookies. Defaults to false.
        ///</summary>
        [DataMember]
        public virtual bool? Cookieless { get; set; }
    }

    [DataContract]
    public partial class AuthenticateResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string SessionToken { get; set; }
    }

    public partial class CampaignFilter
    {
        public CampaignFilter()
        {
            CampaignGuids = new List<Guid>{};
        }

        ///<summary>
        ///Search by all or part of campaign name
        ///</summary>
        [DataMember]
        public virtual string SearchCriteria { get; set; }

        [DataMember]
        public virtual string Status { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember]
        public virtual List<Guid> CampaignGuids { get; set; }
    }

    public partial class CampaignSort
    {
        [DataMember]
        public virtual string Type { get; set; }

        [DataMember]
        public virtual string Direction { get; set; }
    }

    public partial class ChunkProgress
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        ///<summary>
        ///The sequence number of the attachment Chunk.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual int ChunkNumber { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string Status { get; set; }
    }

    [Route("/public/users/registration", "PUT")]
    [Route("/v1/public/users/registration", "PUT")]
    [DataContract]
    public partial class ConfirmRegistration
        : IReturn<ConfirmRegistrationResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string RegistrationConfirmationToken { get; set; }

        ///<summary>
        ///Determines whether session-token should be set in the Cookies. Defaults to false.
        ///</summary>
        [DataMember]
        public virtual bool? Cookieless { get; set; }
    }

    [DataContract]
    public partial class ConfirmRegistrationResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string SessionToken { get; set; }
    }

    [Route("/v1/user/aliases/confirm", "POST")]
    [DataContract]
    public partial class ConfirmUserAlias
        : IReturn<ConfirmUserAliasResponse>
    {
        ///<summary>
        ///The confirmation Token received by the user.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string ConfirmationToken { get; set; }

        ///<summary>
        ///The password of the alias you are adding.
        ///</summary>
        [DataMember]
        public virtual string Password { get; set; }
    }

    [DataContract]
    public partial class ConfirmUserAliasResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/downloadtoken", "POST")]
    [DataContract]
    public partial class CreateAttachmentDownloadToken
        : IReturn<CreateAttachmentDownloadTokenResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember]
        public virtual string Password { get; set; }

        [DataMember]
        public virtual string AuthAuditToken { get; set; }
    }

    [DataContract]
    public partial class CreateAttachmentDownloadTokenResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid DownloadToken { get; set; }
    }

    [Route("/v1/attachments/groups", "POST")]
    [DataContract]
    public partial class CreateAttachmentGroup
        : IReturn<CreateAttachmentGroupResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Name { get; set; }
    }

    public partial class CreateAttachmentGroupResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGroupGuid { get; set; }
    }

    [Route("/v1/campaigns", "POST")]
    [DataContract]
    public partial class CreateCampaign
        : IReturn<CreateCampaignResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        ///<summary>
        ///In Automatic mode, the user only needs to create 1 draft message with Subject, Body, Attachments etc. Once the message is sent, the server will create copies of the draft for each recipient specified in the To field and send the message individually. If left in manual mode, it is up to the Api consumer to PreCreate / Save / Send each message individually. Defaults to Automatic.
        ///</summary>
        [DataMember]
        public virtual string Mode { get; set; }
    }

    [DataContract]
    public partial class CreateCampaignResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }

        ///<summary>
        ///Only returns a valid Message Guid when CampaignMode is Automatic. The Message is left in a precreated state, and still requires subsequesnt calls to SaveMessage. This message should be left in a Draft state because it is a Template for the Campaign and it should never be sent.
        ///</summary>
        [DataMember]
        public virtual Guid? TemplateMessageGuid { get; set; }
    }

    [Route("/v1/labels", "POST")]
    [DataContract]
    public partial class CreateLabel
        : IReturn<CreateLabelResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string Name { get; set; }

        ///<summary>
        ///The parent label. There's no parent if the value is null.
        ///</summary>
        [DataMember]
        public virtual Guid? ParentLabelGuid { get; set; }
    }

    [DataContract]
    public partial class CreateLabelResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid LabelGuid { get; set; }
    }

    [Route("/v1/notifications", "POST")]
    [DataContract]
    public partial class CreateNotificationSession
        : IReturn<CreateNotificationSessionResponse>
    {
        public CreateNotificationSession()
        {
            NewAssetNotificationFilters = new List<string>{};
        }

        ///<summary>
        ///Only allowed for DeviceType android and iOS.  Gives the notification session a time to live that matches it's time to live.  UpdateHeartbeat is not required to keep session alive if this is used on create.
        ///</summary>
        [DataMember]
        public virtual string AuthenticationToken { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string DeviceType { get; set; }

        ///<summary>
        ///The Id of the Android or iOS device. Not required when DeviceType is InApp. 
        ///</summary>
        [DataMember]
        public virtual string DeviceId { get; set; }

        ///<summary>
        ///The clientAppId corresponds with the app hosted in the app store. The default is Secure Messaging no brand.
        ///</summary>
        [DataMember]
        public virtual string ClientAppId { get; set; }

        [DataMember]
        public virtual string PushToken { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool EnableUserTrackingNotifications { get; set; }

        [DataMember]
        public virtual List<string> NewAssetNotificationFilters { get; set; }
    }

    [DataContract]
    public partial class CreateNotificationSessionResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid NotificationSessionToken { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/signature/decline", "POST")]
    [DataContract]
    public partial class DeclineESignature
        : IReturn<DeclineESignatureResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember]
        public virtual string Reason { get; set; }
    }

    public partial class DeclineESignatureResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}", "DELETE")]
    [DataContract]
    public partial class DeleteAttachment
        : IReturn<DeleteAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/extensions", "DELETE")]
    [DataContract]
    public partial class DeleteAttachmentExtensions
        : IReturn<DeleteAttachmentExtensionsResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember]
        public virtual string Key { get; set; }
    }

    [DataContract]
    public partial class DeleteAttachmentExtensionsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/groups/{AttachmentGroupGuid}", "DELETE")]
    [DataContract]
    public partial class DeleteAttachmentGroup
        : IReturn<DeleteAttachmentGroupResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGroupGuid { get; set; }
    }

    public partial class DeleteAttachmentGroupResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    public partial class DeleteAttachmentResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/labels/{LabelGuid}", "DELETE")]
    [DataContract]
    public partial class DeleteLabel
        : IReturn<DeleteLabelResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid LabelGuid { get; set; }
    }

    [DataContract]
    public partial class DeleteLabelResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/messages/{MessageGuid}/extensions", "DELETE")]
    [DataContract]
    public partial class DeleteMessageExtensions
        : IReturn<DeleteMessageExtensionsResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember]
        public virtual string Key { get; set; }
    }

    [DataContract]
    public partial class DeleteMessageExtensionsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/user/extensions", "DELETE")]
    [DataContract]
    public partial class DeleteUserExtensions
        : IReturn<DeleteUserExtensionsResponse>
    {
        [DataMember]
        public virtual string Key { get; set; }
    }

    [DataContract]
    public partial class DeleteUserExtensionsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/attachments/{AttachmentGuid}", "GET")]
    [Route("/v1/attachments/{AttachmentGuid}", "GET")]
    [DataContract]
    public partial class DownloadAttachment
        : IReturn<DownloadAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember]
        public virtual string DownloadType { get; set; }

        [DataMember]
        public virtual string DownloadFileType { get; set; }

        ///<summary>
        ///Deprecated. Please use DownloadType.
        ///</summary>
        [DataMember]
        public virtual bool? Preview { get; set; }
    }

    [Route("/attachments/{AttachmentGuid}/chunk/{ChunkNumber}", "GET")]
    [Route("/v1/attachments/{AttachmentGuid}/chunk/{ChunkNumber}", "GET")]
    [DataContract]
    public partial class DownloadAttachmentChunk
        : IReturn<DownloadAttachmentChunkResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        ///<summary>
        ///The sequence number of the attachment Chunk.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual int ChunkNumber { get; set; }

        [DataMember]
        public virtual string DownloadType { get; set; }

        [DataMember]
        public virtual string DownloadFileType { get; set; }

        ///<summary>
        ///Deprecated. Please use DownloadType.
        ///</summary>
        [DataMember]
        public virtual bool? Preview { get; set; }
    }

    [DataContract]
    public partial class DownloadAttachmentChunkResponse
    {
    }

    [Route("/v1/attachments/{AttachmentGuid}/qrcode", "GET")]
    [DataContract]
    public partial class DownloadAttachmentQrCode
        : IReturn<DownloadAttachmentQrCodeResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        ///<summary>
        ///If left null, defaults to Png.
        ///</summary>
        [DataMember]
        public virtual string Format { get; set; }

        ///<summary>
        ///If left null, defaults to Raw.
        ///</summary>
        [DataMember]
        public virtual string Layout { get; set; }
    }

    [DataContract]
    public partial class DownloadAttachmentQrCodeResponse
    {
    }

    [DataContract]
    public partial class DownloadAttachmentResponse
    {
    }

    [Route("/v1/attachments/download/{DownloadToken}", "GET")]
    [DataContract]
    public partial class DownloadAttachmentWithToken
        : IReturn<DownloadAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid DownloadToken { get; set; }

        ///<summary>
        ///Defaults to false. 
        ///</summary>
        [DataMember]
        public virtual string DownloadType { get; set; }

        ///<summary>
        ///Defaults to false. 
        ///</summary>
        [DataMember]
        public virtual string DownloadFileType { get; set; }

        ///<summary>
        ///Deprecated. Please use DownloadType.
        ///</summary>
        [DataMember]
        public virtual bool? Preview { get; set; }
    }

    [Route("/v1/messages/{MessageGuid}/qrcode", "GET")]
    [DataContract]
    public partial class DownloadMessageQrCode
        : IReturn<DownloadMessageQrCodeResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        ///<summary>
        ///If left null, defaults to Png.
        ///</summary>
        [DataMember]
        public virtual string Format { get; set; }

        ///<summary>
        ///If left null, defaults to Raw.
        ///</summary>
        [DataMember]
        public virtual string Layout { get; set; }
    }

    [DataContract]
    public partial class DownloadMessageQrCodeResponse
    {
    }

    [Route("/v1/users/initials", "GET")]
    [DataContract]
    public partial class DownloadUserInitials
        : IReturn<DownloadUserInitialsResponse>
    {
    }

    [DataContract]
    public partial class DownloadUserInitialsResponse
    {
    }

    [Route("/v1/users/signature", "GET")]
    [DataContract]
    public partial class DownloadUserSignature
        : IReturn<DownloadUserSignatureResponse>
    {
    }

    [DataContract]
    public partial class DownloadUserSignatureResponse
    {
    }

    [Route("/user/authenticationtoken", "DELETE")]
    [Route("/v1/user/authenticationtoken", "DELETE")]
    [DataContract]
    public partial class ExpireAuthenticationToken
        : IReturn<ExpireAuthenticationTokenResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string AuthenticationToken { get; set; }
    }

    [DataContract]
    public partial class ExpireAuthenticationTokenResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/user/authenticationtokens", "DELETE")]
    [Route("/v1/user/authenticationtokens", "DELETE")]
    [DataContract]
    public partial class ExpireAuthenticationTokens
        : IReturn<ExpireAuthenticationTokensResponse>
    {
    }

    [DataContract]
    public partial class ExpireAuthenticationTokensResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/public/users/{EmailAddress}/forgotpassword", "POST")]
    [Route("/v1/public/users/forgotpassword", "POST")]
    [DataContract]
    public partial class ForgotPassword
        : IReturn<ForgotPasswordResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string EmailAddress { get; set; }
    }

    [DataContract]
    public partial class ForgotPasswordResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/attachments/{AttachmentGuid}/details", "GET")]
    [Route("/v1/attachments/{AttachmentGuid}/details", "GET")]
    [DataContract]
    public partial class GetAttachment
        : IReturn<GetAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }
    }

    [Route("/v1/attachments/groups/{CampaignGuid}", "GET")]
    [DataContract]
    public partial class GetAttachmentGroups
        : IReturn<GetAttachmentGroupsResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }
    }

    public partial class GetAttachmentGroupsResponse
    {
        public GetAttachmentGroupsResponse()
        {
            AttachmentGroups = new List<AttachmentGroup>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<AttachmentGroup> AttachmentGroups { get; set; }
    }

    public partial class GetAttachmentResponse
        : Entities.Attachment
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/attachments/progress", "GET")]
    [Route("/v1/attachments/progress", "GET")]
    [DataContract]
    public partial class GetAttachmentsUploadProgress
        : IReturn<GetAttachmentsUploadProgressResponse>
    {
        public GetAttachmentsUploadProgress()
        {
            AttachmentGuids = new List<Guid>{};
        }

        [DataMember(IsRequired=true)]
        public virtual List<Guid> AttachmentGuids { get; set; }
    }

    public partial class GetAttachmentsUploadProgressResponse
    {
        public GetAttachmentsUploadProgressResponse()
        {
            AttachmentProgresses = new List<AttachmentProgress>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<AttachmentProgress> AttachmentProgresses { get; set; }
    }

    [Route("/v1/campaigns/reports", "GET")]
    [DataContract]
    public partial class GetCampaignReports
        : IReturn<GetCampaignReportsPagedResponse>
    {
        ///<summary>
        ///The Page number being requested. Defaults to 1.
        ///</summary>
        [DataMember]
        public virtual int? Page { get; set; }

        ///<summary>
        ///The Page Size returned by the operation. Defaults to 25. Max 1000.
        ///</summary>
        [DataMember]
        public virtual int? PageSize { get; set; }

        ///<summary>
        ///Find all reports for a single Campaign if passed.
        ///</summary>
        [DataMember]
        public virtual Guid? CampaignGuid { get; set; }
    }

    [DataContract]
    public partial class GetCampaignReportsPagedResponse
    {
        public GetCampaignReportsPagedResponse()
        {
            Results = new List<CampaignReportSummary>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int PageSize { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPages { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalItems { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int CurrentPage { get; set; }

        [DataMember]
        public virtual List<CampaignReportSummary> Results { get; set; }
    }

    [Route("/v1/campaigns/{CampaignGuid}/tracking", "GET")]
    [DataContract]
    public partial class GetCampaignTrackingSummary
        : IReturn<GetCampaignTrackingSummaryResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }
    }

    [DataContract]
    public partial class GetCampaignTrackingSummaryResponse
    {
        public GetCampaignTrackingSummaryResponse()
        {
            AttachmentTrackingSummaries = new List<CampaignAttachmentTrackingSummary>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual CampaignMessageTrackingSummary MessageTrackingSummary { get; set; }

        [DataMember]
        public virtual List<CampaignAttachmentTrackingSummary> AttachmentTrackingSummaries { get; set; }
    }

    [Route("/v1/conversations/{ConversationGuid}", "GET")]
    [DataContract]
    public partial class GetConversation
        : IReturn<GetConversationResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid ConversationGuid { get; set; }

        ///<summary>
        ///Defaults to true.
        ///</summary>
        [DataMember]
        public virtual bool? ReturnMessageSummaries { get; set; }

        ///<summary>
        ///Defaults to true.
        ///</summary>
        [DataMember]
        public virtual bool? ReturnAttachmentSummaries { get; set; }

        ///<summary>
        ///Defaults to true.
        ///</summary>
        [DataMember]
        public virtual bool? ReturnConversationMembers { get; set; }
    }

    [DataContract]
    public partial class GetConversationResponse
    {
        public GetConversationResponse()
        {
            MessageSummaries = new List<MessageSummary>{};
            AttachmentSummaries = new List<AttachmentSummary>{};
            ConversationMembers = new List<Contact>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Contact ConversationOwner { get; set; }

        [DataMember]
        public virtual List<MessageSummary> MessageSummaries { get; set; }

        [DataMember]
        public virtual List<AttachmentSummary> AttachmentSummaries { get; set; }

        [DataMember]
        public virtual List<Contact> ConversationMembers { get; set; }
    }

    [Route("/v1/data/{Token}", "GET")]
    [DataContract]
    public partial class GetData
        : IReturn<GetDataResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid Token { get; set; }
    }

    [DataContract]
    public partial class GetDataResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Data { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/draft/signatures", "GET")]
    [DataContract]
    public partial class GetDraftESignatures
        : IReturn<GetDraftESignaturesResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }
    }

    public partial class GetDraftESignaturesResponse
    {
        public GetDraftESignaturesResponse()
        {
            Signatures = new List<SignatureDraft>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<SignatureDraft> Signatures { get; set; }
    }

    [Route("/v1/hash/{HashToken}", "GET")]
    [DataContract]
    public partial class GetHashCalculation
        : IReturn<GetHashCalculationResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid HashToken { get; set; }
    }

    public partial class GetHashCalculationResponse
    {
        public GetHashCalculationResponse()
        {
            Results = new List<HashCalculationResult>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<HashCalculationResult> Results { get; set; }
    }

    [Route("/v1/labels", "GET")]
    [DataContract]
    public partial class GetLabels
        : IReturn<GetLabelsResponse>
    {
    }

    [DataContract]
    public partial class GetLabelsResponse
    {
        public GetLabelsResponse()
        {
            Results = new List<Label>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember]
        public virtual List<Label> Results { get; set; }
    }

    [Route("/messages/{MessageGuid}", "GET")]
    [Route("/v1/messages/{MessageGuid}", "GET")]
    [DataContract]
    public partial class GetMessage
        : IReturn<GetMessageResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        ///<summary>
        ///Retrieve the body of the message.  Defaults to true.
        ///</summary>
        [DataMember]
        public virtual bool? RetrieveBody { get; set; }
    }

    public partial class GetMessageResponse
        : MessageSummary
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Body { get; set; }
    }

    [Route("/v1/messages/{MessageGuid}/tree", "GET")]
    [DataContract]
    public partial class GetMessageTree
        : IReturn<GetMessageTreeResponse>
    {
        ///<summary>
        ///Can be any message guid including the original message, or the leaf message in a tree of replies/forwards. A message sender can access any part of the tree that originated from the sent message, even if the sender is no longer included in the messages (a sender owns everything in the lower branch).
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        ///<summary>
        ///If true, gets the original message that started this messages conversation thread, or the highest one available to the current user. Defaults to false.
        ///</summary>
        [DataMember]
        public virtual bool? ShowOriginalMessage { get; set; }

        ///<summary>
        ///The number of child messages to return relative to the current root message. Defaults to return all child messages. 
        ///</summary>
        [DataMember]
        public virtual int? MaxDepth { get; set; }
    }

    [DataContract]
    public partial class GetMessageTreeResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual MessageTreeItem MessageTree { get; set; }
    }

    [Route("/user/authenticationtoken", "GET")]
    [Route("/v1/user/authenticationtoken", "GET")]
    [DataContract]
    public partial class GetNewAuthenticationToken
        : IReturn<GetNewAuthenticationTokenResponse>
    {
        ///<summary>
        ///Defaults to 14.
        ///</summary>
        [DataMember]
        public virtual int? AuthenticationTokenMaxDays { get; set; }
    }

    [DataContract]
    public partial class GetNewAuthenticationTokenResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string AuthenticationToken { get; set; }
    }

    [Route("/v1/notifications", "GET")]
    [DataContract]
    public partial class GetNotificationSessionDetails
        : IReturn<GetNotificationSessionDetailsResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid NotificationSessionToken { get; set; }
    }

    [DataContract]
    public partial class GetNotificationSessionDetailsResponse
    {
        public GetNotificationSessionDetailsResponse()
        {
            ActiveNewAssetNotificationFilters = new List<string>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual DateTime CreatedOn { get; set; }

        [DataMember(IsRequired=true)]
        public virtual DateTime? LastHeartbeat { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string DeviceType { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool UserTrackingNotificationsEnabled { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<string> ActiveNewAssetNotificationFilters { get; set; }
    }

    [Route("/v1/public/users/quickregistration", "GET")]
    [DataContract]
    public partial class GetQuickRegistration
        : IReturn<GetQuickRegistrationResponse>
    {
        [DataMember]
        public virtual string QuickRegistrationToken { get; set; }
    }

    [DataContract]
    public partial class GetQuickRegistrationResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string EmailAddress { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/messages", "GET")]
    [DataContract]
    public partial class GetRelatedMessages
        : IReturn<GetRelatedMessagesResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }
    }

    [DataContract]
    public partial class GetRelatedMessagesResponse
    {
        public GetRelatedMessagesResponse()
        {
            RelatedMessages = new List<MessageSummary>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember]
        public virtual List<MessageSummary> RelatedMessages { get; set; }
    }

    [Route("/public/service/settings", "GET")]
    [Route("/v1/public/service/settings", "GET")]
    [DataContract]
    public partial class GetServicePublicSettings
        : IReturn<GetServicePublicSettingsResponse>
    {
    }

    [DataContract]
    public partial class GetServicePublicSettingsResponse
    {
        public GetServicePublicSettingsResponse()
        {
            Theme = new Dictionary<string, string>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string ServiceName { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string ServiceCode { get; set; }

        [DataMember]
        public virtual BoolFeature RememberMe { get; set; }

        [DataMember]
        public virtual BoolFeature KeepMeLoggedIn { get; set; }

        [DataMember]
        public virtual BoolFeature AutoComplete { get; set; }

        [DataMember]
        public virtual BoolFeature SecureCookies { get; set; }

        [DataMember]
        public virtual BoolFeature EnableWebApp { get; set; }

        [DataMember]
        public virtual BoolFeature EnableLegacyWebMail { get; set; }

        [DataMember]
        public virtual string WebmailLink { get; set; }

        [DataMember]
        public virtual string WebAppLink { get; set; }

        [DataMember]
        public virtual string BannerLink { get; set; }

        [DataMember]
        public virtual string MobileLink { get; set; }

        [DataMember]
        public virtual Terms Terms { get; set; }

        [DataMember]
        public virtual CRA CRA { get; set; }

        [DataMember]
        public virtual Localization Localization { get; set; }

        [DataMember]
        public virtual Branding Branding { get; set; }

        [DataMember]
        public virtual PasswordManagement PasswordManagement { get; set; }

        [DataMember]
        public virtual Dictionary<string, string> Theme { get; set; }

        [DataMember]
        public virtual Sso Sso { get; set; }

        [DataMember]
        public virtual BoolFeature EnableTouchValidation { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature ConsumerMode { get; set; }
    }

    [Route("/service/settings", "GET")]
    [Route("/v1/service/settings", "GET")]
    [DataContract]
    public partial class GetServiceSettings
        : IReturn<GetServiceSettingsResponse>
    {
    }

    [DataContract]
    public partial class GetServiceSettingsResponse
    {
        public GetServiceSettingsResponse()
        {
            AllowedInAppNotificationEvents = new List<string>{};
            AllowedPushNotificationEvents = new List<string>{};
            AllowedEmailNotificationEvents = new List<string>{};
            Theme = new Dictionary<string, string>{};
            AttachmentTypes = new List<string>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string ServiceName { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string ServiceCode { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string WebmailLink { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string WebAppLink { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string BannerLink { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string MobileLink { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string AdministratorEmailAddress { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature RememberMe { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature KeepMeLoggedIn { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature AutoComplete { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature SecureCookies { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableWebApp { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableLegacyWebMail { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature Files { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature Users { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature InviteUser { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Compose CreateMessage { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature ReplyMessage { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature ReplyAllMessage { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature ForwardMessage { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature DeleteMessage { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature MessageRecall { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature FYEO { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature Confidential { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature MessageTracking { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature Identities { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature HideEmail { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature VisuallyImparedInterface { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature SecureEmail { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature SecureLargeFiles { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableTouchValidation { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature BasicEncryption { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<string> AllowedInAppNotificationEvents { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<string> AllowedPushNotificationEvents { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<string> AllowedEmailNotificationEvents { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Branding Branding { get; set; }

        [DataMember(IsRequired=true)]
        public virtual RestrictedExtensions RestrictedExtensions { get; set; }

        [DataMember(IsRequired=true)]
        public virtual PasswordManagement PasswordManagement { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Notification NotificationServices { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Terms Terms { get; set; }

        [DataMember(IsRequired=true)]
        public virtual CRA CRA { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Localization Localization { get; set; }

        [DataMember(IsRequired=true)]
        public virtual UniversalAccess UniversalAccess { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Menu Menu { get; set; }

        [DataMember(IsRequired=true)]
        public virtual long? MaxMessageFileSizeBytes { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Dictionary<string, string> Theme { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature AttachmentPreview { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature ESignatures { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature ESignatureCompose { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature MessageExpiry { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature MessageRetention { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<string> AttachmentTypes { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature Campaigns { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableLabels { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnablePreSendPreview { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableESignatureInPlace { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableESignatureDeadline { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableESignatureManualReminders { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableESignatureAutomaticReminders { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableESignatureNotifications { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableMessageAutomaticReminders { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int DefaultMessageExpiry { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int DefaultESignatureDeadline { get; set; }

        ///<summary>
        ///Timeout in minutes.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual int NotificationSessionTimeout { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int ReminderAfterNDaysESignatureReceived { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int ReminderEveryNDaysESignatureReceived { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int ReminderMaxNDaysESignatureReceived { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int Reminder1NDaysBeforeESignatureDeadline { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int Reminder2NDaysBeforeESignatureDeadline { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int Reminder3NDaysBeforeESignatureDeadline { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int Reminder4NDaysBeforeESignatureDeadline { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int Reminder5NDaysBeforeESignatureDeadline { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string MessageQrCodes { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string AttachmentQrCodes { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string ESignatureQrCodes { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableHashValidation { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableSharing { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnableESignatureInPlaceQuick { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature ForceESignatureReauthentication { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature ConsumerMode { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature AdvancedTracking { get; set; }

        [DataMember(IsRequired=true)]
        public virtual BoolFeature EnablePrimeTime { get; set; }

        [DataMember(IsRequired=true)]
        public virtual DefaultSecureSettings DefaultSecureSettings { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/signatures", "GET")]
    [DataContract]
    public partial class GetSignatures
        : IReturn<GetSignaturesResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }
    }

    public partial class GetSignaturesResponse
    {
        public GetSignaturesResponse()
        {
            Signatures = new List<Signature>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<Signature> Signatures { get; set; }
    }

    [Route("/v1/notifications/subscriptions", "GET")]
    [DataContract]
    public partial class GetSubscriptions
        : IReturn<GetSubscriptionsResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid NotificationSessionToken { get; set; }
    }

    [DataContract]
    public partial class GetSubscriptionsResponse
    {
        public GetSubscriptionsResponse()
        {
            Subscriptions = new List<SubscriptionPayload>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<SubscriptionPayload> Subscriptions { get; set; }
    }

    [Route("/v1/user/aliases/confirm/{ConfirmationToken}", "GET")]
    [DataContract]
    public partial class GetUserAliasConfirmation
        : IReturn<GetUserAliasConfirmationResponse>
    {
        ///<summary>
        ///The confirmation Token received by the user.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string ConfirmationToken { get; set; }
    }

    [DataContract]
    public partial class GetUserAliasConfirmationResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember]
        public virtual string MasterEmailAddress { get; set; }

        [DataMember]
        public virtual string AliasEmailAddress { get; set; }

        [DataMember]
        public virtual bool IsUserRegistered { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool IsPasswordRequired { get; set; }
    }

    [Route("/user/aliases", "GET")]
    [Route("/v1/user/aliases", "GET")]
    [DataContract]
    public partial class GetUserAliases
        : IReturn<GetUserAliasesResponse>
    {
    }

    [DataContract]
    public partial class GetUserAliasesResponse
    {
        public GetUserAliasesResponse()
        {
            Results = new List<EmailAlias>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember]
        public virtual List<EmailAlias> Results { get; set; }
    }

    [Route("/v1/user/contact", "GET")]
    [DataContract]
    public partial class GetUserContact
        : IReturn<GetUserContactResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid UserServiceGuid { get; set; }
    }

    [DataContract]
    public partial class GetUserContactResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember]
        public virtual Contact Contact { get; set; }

        [DataMember]
        public virtual int TotalMessagesSentToContact { get; set; }

        [DataMember]
        public virtual int TotalMessagesReceivedFromContact { get; set; }

        [DataMember]
        public virtual DateTime? LastCorrespondence { get; set; }
    }

    [Route("/user/contacts", "GET")]
    [Route("/v1/user/contacts", "GET")]
    [DataContract]
    public partial class GetUserContacts
        : IReturn<GetUserContactsResponse>
    {
        ///<summary>
        ///The Page number being requested. Defaults to 1.
        ///</summary>
        [DataMember]
        public virtual int? Page { get; set; }

        ///<summary>
        ///The Page Size returned by the operation. Defaults to 25.
        ///</summary>
        [DataMember]
        public virtual int? PageSize { get; set; }
    }

    [DataContract]
    public partial class GetUserContactsResponse
    {
        public GetUserContactsResponse()
        {
            Results = new List<Contact>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int PageSize { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPages { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalItems { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int CurrentPage { get; set; }

        [DataMember]
        public virtual List<Contact> Results { get; set; }
    }

    [Route("/v1/user/extensions", "GET")]
    [DataContract]
    public partial class GetUserExtensions
        : IReturn<GetUserExtensionsResponse>
    {
    }

    [DataContract]
    public partial class GetUserExtensionsResponse
    {
        public GetUserExtensionsResponse()
        {
            Extensions = new List<Extension>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<Extension> Extensions { get; set; }
    }

    [Route("/v1/user/quotas", "GET")]
    [DataContract]
    public partial class GetUserQuotas
        : IReturn<GetUserQuotasResponse>
    {
    }

    [DataContract]
    public partial class GetUserQuotasResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int DailyInvitesAllowed { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int DailyMessagesAllowed { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int DailyMessagesLeft { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int DailyInvitesLeft { get; set; }
    }

    [Route("/user/settings", "GET")]
    [Route("/v1/user/settings", "GET")]
    [DataContract]
    public partial class GetUserSettings
        : IReturn<GetUserSettingsResponse>
    {
    }

    [DataContract]
    public partial class GetUserSettingsResponse
    {
        public GetUserSettingsResponse()
        {
            PushNotificationEvents = new List<string>{};
            InAppNotificationEvents = new List<string>{};
            EmailNotificationEvents = new List<string>{};
            AllowedTrackingEvents = new List<string>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string EmailAddress { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string State { get; set; }

        [DataMember(IsRequired=true)]
        public virtual DateTime RegistrationDate { get; set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string UserTitle { get; set; }

        [DataMember]
        public virtual string CompanyName { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Language { get; set; }

        [DataMember]
        public virtual UserGroup UserGroup { get; set; }

        [DataMember]
        public virtual BoolFeature AutoRetrieveMessage { get; set; }

        [DataMember]
        public virtual OutOfOffice OutOfOffice { get; set; }

        [DataMember]
        public virtual List<string> PushNotificationEvents { get; set; }

        [DataMember]
        public virtual List<string> InAppNotificationEvents { get; set; }

        [DataMember]
        public virtual List<string> EmailNotificationEvents { get; set; }

        [DataMember]
        public virtual List<string> AllowedTrackingEvents { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool EnableWebApp { get; set; }

        [DataMember]
        public virtual string SignatureHtml { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool CustomSignatureUploaded { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool CustomInitialsUploaded { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool IncludeSignatureOnReplyForward { get; set; }
    }

    [DataContract]
    public partial class HashCalculationChunk
    {
        ///<summary>
        ///The sequence number of the attachment Chunk.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual int ChunkNumber { get; set; }

        ///<summary>
        ///The size in Bytes of of this Chunk only.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long BytesSize { get; set; }

        ///<summary>
        ///The start byte index that should be used to slice the chunk in relation to the entire Files byte stream.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long ByteStartIndex { get; set; }

        ///<summary>
        ///The end byte index that should be used to slice the chunk in relation to the entire Files byte stream.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long ByteEndIndex { get; set; }

        ///<summary>
        ///The URI that should be used to POST the chunk to when uploading.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string UploadUri { get; set; }
    }

    public partial class HashCalculationResult
    {
        [DataMember(IsRequired=true)]
        public virtual string HashAlgorithm { get; set; }

        ///<summary>
        ///The Hex string encoded hash value.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string HashValue { get; set; }
    }

    [Route("/v1/public/users/invite", "POST")]
    [DataContract]
    public partial class InviteServiceUserPublic
        : IReturn<InviteServiceUserPublicResponse>
    {
        ///<summary>
        ///Email address to be invited to the portal.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string EmailAddress { get; set; }
    }

    [DataContract]
    public partial class InviteServiceUserPublicResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/messages/{MessageGuid}/attachments/{AttachmentGuid}/link", "PUT")]
    [Route("/v1/messages/{MessageGuid}/attachments/{AttachmentGuid}/link", "PUT")]
    [DataContract]
    public partial class LinkAttachment
        : IReturn<LinkAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }
    }

    [DataContract]
    public partial class LinkAttachmentResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/login", "POST")]
    [Route("/v1/login", "POST")]
    [DataContract]
    public partial class Login
        : IReturn<LoginResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string Username { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Password { get; set; }

        ///<summary>
        ///Determines whether session-token should be set in the Cookies. Defaults to false.
        ///</summary>
        [DataMember]
        public virtual bool? Cookieless { get; set; }
    }

    [DataContract]
    public partial class LoginResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string SessionToken { get; set; }
    }

    [Route("/logout", "POST")]
    [Route("/v1/logout", "POST")]
    [DataContract]
    public partial class Logout
        : IReturn<LogoutResponse>
    {
    }

    [DataContract]
    public partial class LogoutResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/public/ping", "GET")]
    [DataContract]
    public partial class Ping
        : IReturn<PingResponse>
    {
    }

    [DataContract]
    public partial class PingResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Build { get; set; }

        [DataMember]
        public virtual int Version { get; set; }

        [DataMember]
        public virtual int MinSupportedVersion { get; set; }
    }

    [DataContract]
    public partial class PreCreateAttachmentPlaceholder
    {
        [DataMember(IsRequired=true)]
        public virtual string FileName { get; set; }

        ///<summary>
        ///The total number of bytes in the Attachment.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long TotalBytesLength { get; set; }
    }

    [Route("/attachments/precreate", "POST")]
    [Route("/v1/attachments/precreate", "POST")]
    [DataContract]
    public partial class PreCreateAttachments
        : IReturn<PreCreateAttachmentsResponse>
    {
        public PreCreateAttachments()
        {
            AttachmentPlaceholders = new List<PreCreateAttachmentPlaceholder>{};
        }

        ///<summary>
        ///The MessageGuid that the Attachments should be contained in.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        ///<summary>
        ///The placeholders to create for the Attachments that are going to be uploaded. 
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual List<PreCreateAttachmentPlaceholder> AttachmentPlaceholders { get; set; }
    }

    [DataContract]
    public partial class PreCreateAttachmentsResponse
    {
        public PreCreateAttachmentsResponse()
        {
            AttachmentPlaceholders = new List<AttachmentPlaceholder>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        ///<summary>
        ///Use this information to Upload the different Attachments.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual List<AttachmentPlaceholder> AttachmentPlaceholders { get; set; }
    }

    [Route("/v1/hash", "POST")]
    [DataContract]
    public partial class PreCreateHashCalculation
        : IReturn<PreCreateHashCalculationResponse>
    {
        ///<summary>
        ///The total number of bytes in the File.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long TotalBytesLength { get; set; }
    }

    [DataContract]
    public partial class PreCreateHashCalculationResponse
    {
        public PreCreateHashCalculationResponse()
        {
            Chunks = new List<HashCalculationChunk>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        ///<summary>
        ///Creates a token that can be used to upload subsequent file chunks for hash verification. 
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual Guid HashToken { get; set; }

        ///<summary>
        ///The total number of bytes in the file.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual long TotalBytesLength { get; set; }

        ///<summary>
        ///The placeholders for the Chunks of a File. Use this information to seperate the File into Chunks. Files that are smaller than the Chunk Byte Size will only require 1 Chunk. An Files Chunk Numbers are in sequence (eg. 1,2,3,4) and always starts at 1.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual List<HashCalculationChunk> Chunks { get; set; }
    }

    [Route("/messages/", "POST")]
    [Route("/v1/messages/", "POST")]
    [DataContract]
    public partial class PreCreateMessage
        : IReturn<PreCreateMessageResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string ActionCode { get; set; }

        ///<summary>
        ///The guid of the parent message if this is a reply or forward.
        ///</summary>
        [DataMember]
        public virtual Guid? ParentGuid { get; set; }

        ///<summary>
        ///The password of the parent message. This is only required if the parent message requires a password. 
        ///</summary>
        [DataMember]
        public virtual string Password { get; set; }

        ///<summary>
        ///The authAuditToken for the parent message. This is only required if the parent message requires a password. 
        ///</summary>
        [DataMember]
        public virtual string AuthAuditToken { get; set; }

        ///<summary>
        ///The guid of the campaign this message is associated with. The user must be enabled for Campaigns and the Campaign must be Active.
        ///</summary>
        [DataMember]
        public virtual Guid? CampaignGuid { get; set; }

        [DataMember]
        public virtual string ExternalMessageId { get; set; }
    }

    [DataContract]
    public partial class PreCreateMessageResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }
    }

    [Route("/profiler", "GET")]
    [Route("/profiler/{Key}", "GET")]
    [DataContract]
    public partial class Profiler
        : IReturn<ProfilerResponse>
    {
        [DataMember]
        public virtual string Key { get; set; }
    }

    [DataContract]
    public partial class ProfilerResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/user/aliases", "PUT")]
    [Route("/v1/user/aliases", "PUT")]
    [DataContract]
    public partial class PromoteUserAlias
        : IReturn<PromoteUserAliasResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string EmailAlias { get; set; }

        ///<summary>
        ///Required when service is not SSO enabled.
        ///</summary>
        [DataMember]
        public virtual string OldPassword { get; set; }

        ///<summary>
        ///Required when service is not SSO enabled.
        ///</summary>
        [DataMember]
        public virtual string NewPassword { get; set; }
    }

    [DataContract]
    public partial class PromoteUserAliasResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/messages/{MessageGuid}/recall", "POST")]
    [DataContract]
    public partial class RecallMessage
        : IReturn<RecallMessageResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember]
        public virtual string RecallReason { get; set; }

        ///<summary>
        ///Defaults to false.
        ///</summary>
        [DataMember]
        public virtual bool? SingleRecall { get; set; }

        ///<summary>
        ///Defaults to true.
        ///</summary>
        [DataMember]
        public virtual bool? UnshareAttachments { get; set; }
    }

    public partial class RecallMessageResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/public/users", "POST")]
    [Route("/v1/public/users/registration", "POST")]
    [DataContract]
    public partial class RegisterUser
        : IReturn<RegisterUserResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string EmailAddress { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string FirstName { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string LastName { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Password { get; set; }

        [DataMember]
        public virtual string QuickRegistrationKey { get; set; }

        [DataMember]
        public virtual string CRACode { get; set; }

        [DataMember]
        public virtual string Language { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool TermsAccepted { get; set; }

        ///<summary>
        ///Determines whether session-token should be set in the Cookies. Defaults to false.
        ///</summary>
        [DataMember]
        public virtual bool? Cookieless { get; set; }

        [DataMember]
        public virtual string CompanyName { get; set; }
    }

    [DataContract]
    public partial class RegisterUserResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember]
        public virtual string SessionToken { get; set; }
    }

    [Route("/messages/{MessageGuid}/attachments/{AttachmentGuid}", "DELETE")]
    [Route("/v1/messages/{MessageGuid}/attachments/{AttachmentGuid}", "DELETE")]
    [DataContract]
    public partial class RemoveAttachment
        : IReturn<RemoveAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }
    }

    [Route("/v1/labels/{LabelGuid}/attachments/{AttachmentGuid}", "DELETE")]
    [DataContract]
    public partial class RemoveAttachmentLabel
        : IReturn<RemoveAttachmentLabelResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid LabelGuid { get; set; }
    }

    [DataContract]
    public partial class RemoveAttachmentLabelResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    public partial class RemoveAttachmentResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/campaigns/{CampaignGuid}/recipients", "DELETE")]
    [DataContract]
    public partial class RemoveCampaignRecipients
        : IReturn<RemoveCampaignRecipientsResponse>
    {
        public RemoveCampaignRecipients()
        {
            EmailAddresses = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<string> EmailAddresses { get; set; }

        ///<summary>
        ///If true, will remove all campaign recipients. Will not remove recipients that have already been sent a message. If you pass any email addresses, an error will occur.
        ///</summary>
        [DataMember]
        public virtual bool? RemoveAll { get; set; }

        ///<summary>
        ///If true, will recall messages for all campaign recipients that have already been sent a campaign message. 
        ///</summary>
        [DataMember]
        public virtual bool? RecallMessagesSent { get; set; }

        ///<summary>
        ///The message you want displayed to users explaining why the message was recalled. 
        ///</summary>
        [DataMember]
        public virtual string RecallMessage { get; set; }
    }

    [DataContract]
    public partial class RemoveCampaignRecipientsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/labels/{LabelGuid}/messages/{MessageGuid}", "DELETE")]
    [DataContract]
    public partial class RemoveMessageLabel
        : IReturn<RemoveMessageLabelResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid LabelGuid { get; set; }
    }

    [DataContract]
    public partial class RemoveMessageLabelResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/notifications", "DELETE")]
    [DataContract]
    public partial class RemoveNotificationSession
        : IReturn<RemoveNotificationSessionResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid NotificationSessionToken { get; set; }
    }

    [DataContract]
    public partial class RemoveNotificationSessionResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/user/aliases", "DELETE")]
    [Route("/v1/user/aliases", "DELETE")]
    [DataContract]
    public partial class RemoveUserAlias
        : IReturn<RemoveUserAliasResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string EmailAlias { get; set; }
    }

    [DataContract]
    public partial class RemoveUserAliasResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/public/users/registration/resendconfirmation", "POST")]
    [DataContract]
    public partial class ResendRegistrationConfirmation
        : IReturn<ResendRegistrationConfirmationResponse>
    {
        [DataMember]
        public virtual string RegistrationConfirmationToken { get; set; }

        [DataMember]
        public virtual string EmailAddress { get; set; }
    }

    [DataContract]
    public partial class ResendRegistrationConfirmationResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/public/users/resetpassword", "POST")]
    [Route("/v1/public/users/resetpassword", "POST")]
    [DataContract]
    public partial class ResetPassword
        : IReturn<ResetPasswordResponse>
    {
        [DataMember]
        public virtual string ForgotPasswordToken { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string NewPassword { get; set; }
    }

    [DataContract]
    public partial class ResetPasswordResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/messages/{MessageGuid}/save", "PUT")]
    [Route("/v1/messages/{MessageGuid}/save", "PUT")]
    [DataContract]
    public partial class SaveMessage
        : IReturn<SaveMessageResponse>
    {
        public SaveMessage()
        {
            To = new List<string>{};
            Cc = new List<string>{};
            Bcc = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember]
        public virtual List<string> To { get; set; }

        [DataMember]
        public virtual List<string> Cc { get; set; }

        [DataMember]
        public virtual List<string> Bcc { get; set; }

        [DataMember]
        public virtual string Subject { get; set; }

        [DataMember]
        public virtual string Body { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string BodyFormat { get; set; }

        ///<summary>
        ///When omitted, the services default message options are used.
        ///</summary>
        [DataMember]
        public virtual MessageOptions MessageOptions { get; set; }

        ///<summary>
        ///Message to expire on this date in accordance with expiry policy.
        ///</summary>
        [DataMember]
        public virtual DateTime? ExpiryDate { get; set; }

        [DataMember]
        public virtual string ExpiryGroup { get; set; }
    }

    [Route("/messages/saveext", "PUT")]
    [Route("/v1/messages/saveext", "PUT")]
    [DataContract]
    public partial class SaveMessageExt
        : IReturn<SaveMessageExtResponse>
    {
        public SaveMessageExt()
        {
            AdditionalTo = new List<string>{};
            AdditionalCc = new List<string>{};
            AdditionalBcc = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        public virtual string ExternalMessageId { get; set; }

        [DataMember]
        public virtual List<string> AdditionalTo { get; set; }

        [DataMember]
        public virtual List<string> AdditionalCc { get; set; }

        [DataMember]
        public virtual List<string> AdditionalBcc { get; set; }
    }

    [DataContract]
    public partial class SaveMessageExtResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public partial class SaveMessageResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/search", "GET")]
    [DataContract]
    public partial class SearchAttachments
        : IReturn<SearchAttachmentsPagedResponse>
    {
        ///<summary>
        ///The Page number being requested. Defaults to 1.
        ///</summary>
        [DataMember]
        public virtual int? Page { get; set; }

        ///<summary>
        ///The Page Size returned by the operation. Defaults to 25.
        ///</summary>
        [DataMember]
        public virtual int? PageSize { get; set; }

        [DataMember]
        public virtual SearchAttachmentFilter Filter { get; set; }

        [DataMember]
        public virtual SearchAttachmentSort Sort { get; set; }
    }

    public partial class SearchAttachmentsPagedResponse
    {
        public SearchAttachmentsPagedResponse()
        {
            Results = new List<Entities.Attachment>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int PageSize { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPages { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalItems { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int CurrentPage { get; set; }

        [DataMember]
        public virtual List<Entities.Attachment> Results { get; set; }
    }

    [Route("/v1/tracking/attachment/search", "GET")]
    [DataContract]
    public partial class SearchAttachmentTracking
        : IReturn<SearchTrackingPagedResponse>
    {
        ///<summary>
        ///The Page number being requested. Defaults to 1.
        ///</summary>
        [DataMember]
        public virtual int? Page { get; set; }

        ///<summary>
        ///The Page Size returned by the operation. Defaults to 25. Max 1000.
        ///</summary>
        [DataMember]
        public virtual int? PageSize { get; set; }

        [DataMember(IsRequired=true)]
        public virtual AttachmentTrackingFilter Filter { get; set; }

        [DataMember]
        public virtual TrackingSort Sort { get; set; }
    }

    [Route("/v1/campaigns/{CampaignGuid}/recipients", "GET")]
    [DataContract]
    public partial class SearchCampaignRecipients
        : IReturn<SearchCampaignRecipientsPagedResponse>
    {
        public SearchCampaignRecipients()
        {
            Statuses = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }

        ///<summary>
        ///The Page number being requested. Defaults to 1.
        ///</summary>
        [DataMember]
        public virtual int? Page { get; set; }

        ///<summary>
        ///The Page Size returned by the operation. Defaults to 25. Max 1000.
        ///</summary>
        [DataMember]
        public virtual int? PageSize { get; set; }

        ///<summary>
        ///Search by all or part of an campaign recipient email address
        ///</summary>
        [DataMember]
        public virtual string SearchCriteria { get; set; }

        ///<summary>
        ///Returns only campaign recipients whose status matches a type in this list.
        ///</summary>
        [DataMember]
        public virtual List<string> Statuses { get; set; }
    }

    [DataContract]
    public partial class SearchCampaignRecipientsPagedResponse
    {
        public SearchCampaignRecipientsPagedResponse()
        {
            Results = new List<CampaignRecipient>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int PageSize { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPages { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalItems { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int CurrentPage { get; set; }

        [DataMember]
        public virtual List<CampaignRecipient> Results { get; set; }
    }

    [Route("/v1/campaigns/search", "GET")]
    [DataContract]
    public partial class SearchCampaigns
        : IReturn<SearchCampaignsPagedResponse>
    {
        ///<summary>
        ///The Page number being requested. Defaults to 1.
        ///</summary>
        [DataMember]
        public virtual int? Page { get; set; }

        ///<summary>
        ///The Page Size returned by the operation. Defaults to 25.
        ///</summary>
        [DataMember]
        public virtual int? PageSize { get; set; }

        [DataMember]
        public virtual CampaignFilter Filter { get; set; }

        [DataMember]
        public virtual CampaignSort Sort { get; set; }
    }

    public partial class SearchCampaignsPagedResponse
    {
        public SearchCampaignsPagedResponse()
        {
            Results = new List<Campaign>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int PageSize { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPages { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalItems { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int CurrentPage { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<Campaign> Results { get; set; }
    }

    [Route("/v1/messages/search", "GET")]
    [DataContract]
    public partial class SearchMessages
        : IReturn<SearchMessagesPagedResponse>
    {
        ///<summary>
        ///The Page number being requested. Defaults to 1.
        ///</summary>
        [DataMember]
        public virtual int? Page { get; set; }

        ///<summary>
        ///The Page Size returned by the operation. Defaults to 25.
        ///</summary>
        [DataMember]
        public virtual int? PageSize { get; set; }

        [DataMember]
        public virtual SearchMessagesFilter Filter { get; set; }

        [DataMember]
        public virtual SearchMessagesSort Sort { get; set; }
    }

    [DataContract]
    public partial class SearchMessagesPagedResponse
    {
        public SearchMessagesPagedResponse()
        {
            Results = new List<MessageSummary>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int PageSize { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPages { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalItems { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int CurrentPage { get; set; }

        [DataMember]
        public virtual List<MessageSummary> Results { get; set; }
    }

    [Route("/v1/tracking/message/search", "GET")]
    [DataContract]
    public partial class SearchMessageTracking
        : IReturn<SearchTrackingPagedResponse>
    {
        ///<summary>
        ///The Page number being requested. Defaults to 1.
        ///</summary>
        [DataMember]
        public virtual int? Page { get; set; }

        ///<summary>
        ///The Page Size returned by the operation. Defaults to 25. Max 1000.
        ///</summary>
        [DataMember]
        public virtual int? PageSize { get; set; }

        [DataMember(IsRequired=true)]
        public virtual MessageTrackingFilter Filter { get; set; }

        [DataMember]
        public virtual TrackingSort Sort { get; set; }
    }

    [DataContract]
    public partial class SearchTrackingPagedResponse
    {
        public SearchTrackingPagedResponse()
        {
            Results = new List<TrackingDetail>{};
        }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int PageSize { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalPages { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int TotalItems { get; set; }

        [DataMember(IsRequired=true)]
        public virtual int CurrentPage { get; set; }

        [DataMember]
        public virtual List<TrackingDetail> Results { get; set; }
    }

    [Route("/v1/tracking/user/search", "GET")]
    [DataContract]
    public partial class SearchUserTracking
        : IReturn<SearchTrackingPagedResponse>
    {
        ///<summary>
        ///The Page number being requested. Defaults to 1.
        ///</summary>
        [DataMember]
        public virtual int? Page { get; set; }

        ///<summary>
        ///The Page Size returned by the operation. Defaults to 25. Max 1000.
        ///</summary>
        [DataMember]
        public virtual int? PageSize { get; set; }

        [DataMember]
        public virtual UserTrackingFilter Filter { get; set; }

        [DataMember]
        public virtual TrackingSort Sort { get; set; }
    }

    [Route("/user/activationmessage", "POST")]
    [Route("/v1/user/activationmessage", "POST")]
    [DataContract]
    public partial class SendActivationMessage
        : IReturn<SendActivationMessageResponse>
    {
    }

    [DataContract]
    public partial class SendActivationMessageResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/reminder", "POST")]
    [DataContract]
    public partial class SendESignatureReminders
        : IReturn<SendESignatureRemindersResponse>
    {
        public SendESignatureReminders()
        {
            EmailAddresses = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        ///<summary>
        ///Maximum of 500 email addresses at a time. Invalid and blacklisted emails are accepted (they are checked when campaign processing begins).
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual List<string> EmailAddresses { get; set; }
    }

    public partial class SendESignatureRemindersResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/messages/{MessageGuid}/send", "PUT")]
    [Route("/v1/messages/{MessageGuid}/send", "PUT")]
    [DataContract]
    public partial class SendMessage
        : IReturn<SendMessageResponse>
    {
        public SendMessage()
        {
            NotificationFormats = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        ///<summary>
        ///The password for the message, if required.
        ///</summary>
        [DataMember]
        public virtual string Password { get; set; }

        ///<summary>
        ///Recipients of the message that do not exist in the service will be invited.
        ///</summary>
        [DataMember]
        public virtual bool? InviteNewUsers { get; set; }

        ///<summary>
        ///Require the service to send a basic email notification message.
        ///</summary>
        [DataMember]
        public virtual bool? SendEmailNotification { get; set; }

        [DataMember]
        public virtual string CraCode { get; set; }

        [DataMember]
        public virtual List<string> NotificationFormats { get; set; }
    }

    [Route("/v1/messages/{MessageGuid}/notification", "PUT")]
    [DataContract]
    public partial class SendMessageNotification
        : IReturn<SendMessageNotificationResponse>
    {
        public SendMessageNotification()
        {
            To = new List<string>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember]
        public virtual List<string> To { get; set; }
    }

    [DataContract]
    public partial class SendMessageNotificationResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public partial class SendMessageResponse
    {
        [DataMember]
        public virtual string NotificationBodyText { get; set; }

        [DataMember]
        public virtual string NotificationBodyHtml { get; set; }

        [DataMember(IsRequired=true)]
        public virtual bool NotificationSent { get; set; }

        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/extensions", "PUT")]
    [DataContract]
    public partial class SetAttachmentExtensions
        : IReturn<SetAttachmentExtensionsResponse>
    {
        public SetAttachmentExtensions()
        {
            Extensions = new List<Extension>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<Extension> Extensions { get; set; }
    }

    [DataContract]
    public partial class SetAttachmentExtensionsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/data", "POST")]
    [DataContract]
    public partial class SetData
        : IReturn<SetDataResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual string Data { get; set; }
    }

    [DataContract]
    public partial class SetDataResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid Token { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/draft/signatures", "PUT")]
    [DataContract]
    public partial class SetDraftESignatures
        : IReturn<SetDraftESignaturesResponse>
    {
        public SetDraftESignatures()
        {
            Signatures = new List<SignatureDraft>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<SignatureDraft> Signatures { get; set; }
    }

    public partial class SetDraftESignaturesResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/deadline", "PUT")]
    [DataContract]
    public partial class SetESignatureDeadline
        : IReturn<SetESignatureDeadlineResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual DateTime? ESignatureDeadline { get; set; }
    }

    public partial class SetESignatureDeadlineResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/messages/{MessageGuid}/extensions", "PUT")]
    [DataContract]
    public partial class SetMessageExtensions
        : IReturn<SetMessageExtensionsResponse>
    {
        public SetMessageExtensions()
        {
            Extensions = new List<Extension>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<Extension> Extensions { get; set; }
    }

    [DataContract]
    public partial class SetMessageExtensionsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/user/extensions", "PUT")]
    [DataContract]
    public partial class SetUserExtensions
        : IReturn<SetUserExtensionsResponse>
    {
        public SetUserExtensions()
        {
            Extensions = new List<Extension>{};
        }

        [DataMember(IsRequired=true)]
        public virtual List<Extension> Extensions { get; set; }
    }

    [DataContract]
    public partial class SetUserExtensionsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/users/initials", "POST")]
    [DataContract]
    public partial class SetUserInitials
        : IReturn<SetUserInitialsResponse>
    {
    }

    [DataContract]
    public partial class SetUserInitialsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/users/signature", "POST")]
    [DataContract]
    public partial class SetUserSignature
        : IReturn<SetUserSignatureResponse>
    {
    }

    [DataContract]
    public partial class SetUserSignatureResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/signature", "POST")]
    [DataContract]
    public partial class SignAttachment
        : IReturn<SignAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string Answer { get; set; }
    }

    public partial class SignAttachmentResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/campaigns/{CampaignGuid}/start", "POST")]
    [DataContract]
    public partial class StartCampaign
        : IReturn<StartCampaignResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }
    }

    [Route("/v1/campaigns/{CampaignGuid}/report", "POST")]
    [DataContract]
    public partial class StartCampaignReport
        : IReturn<StartCampaignReportResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }
    }

    [DataContract]
    public partial class StartCampaignReportResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid CampaignReportGuid { get; set; }
    }

    [DataContract]
    public partial class StartCampaignResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/campaigns/{CampaignGuid}/stop", "POST")]
    [DataContract]
    public partial class StopCampaign
        : IReturn<StopCampaignResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }
    }

    [DataContract]
    public partial class StopCampaignResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/notifications/subscription", "POST")]
    [DataContract]
    public partial class Subscribe
        : IReturn<SubscribeResponse>
    {
        public Subscribe()
        {
            Subscriptions = new List<SubscriptionPayload>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid NotificationSessionToken { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<SubscriptionPayload> Subscriptions { get; set; }
    }

    [DataContract]
    public partial class SubscribeResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/tracking/print", "POST")]
    [DataContract]
    public partial class TrackPrintAttachment
        : IReturn<TrackPrintAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }
    }

    public partial class TrackPrintAttachmentResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/messages/{MessageGuid}/tracking/print", "POST")]
    [DataContract]
    public partial class TrackPrintMessage
        : IReturn<TrackPrintMessageResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }
    }

    public partial class TrackPrintMessageResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/messages/{MessageGuid}/attachments/{AttachmentGuid}/unlink", "PUT")]
    [Route("/v1/messages/{MessageGuid}/attachments/{AttachmentGuid}/unlink", "PUT")]
    [DataContract]
    public partial class UnLinkAttachment
        : IReturn<UnLinkAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }
    }

    [DataContract]
    public partial class UnLinkAttachmentResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/attachments/{AttachmentGuid}/share", "DELETE")]
    [DataContract]
    public partial class UnshareAttachment
        : IReturn<UnshareAttachmentResponse>
    {
        public UnshareAttachment()
        {
            UserServiceGuids = new List<Guid>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<Guid> UserServiceGuids { get; set; }

        [DataMember]
        public virtual string UnshareReason { get; set; }
    }

    [DataContract]
    public partial class UnshareAttachmentResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/messages/{MessageGuid}/share", "DELETE")]
    [DataContract]
    public partial class UnshareMessage
        : IReturn<UnshareMessageResponse>
    {
        public UnshareMessage()
        {
            UserServiceGuids = new List<Guid>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid MessageGuid { get; set; }

        [DataMember(IsRequired=true)]
        public virtual List<Guid> UserServiceGuids { get; set; }

        [DataMember]
        public virtual string UnshareReason { get; set; }
    }

    [DataContract]
    public partial class UnshareMessageResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/notifications/subscription", "DELETE")]
    [DataContract]
    public partial class Unsubscribe
        : IReturn<UnsubscribeResponse>
    {
        public Unsubscribe()
        {
            Subscriptions = new List<Guid>{};
        }

        [DataMember(IsRequired=true)]
        public virtual Guid NotificationSessionToken { get; set; }

        [DataMember]
        public virtual List<Guid> Subscriptions { get; set; }

        ///<summary>
        ///Defaults to false.
        ///</summary>
        [DataMember]
        public virtual bool? RemoveAll { get; set; }
    }

    [DataContract]
    public partial class UnsubscribeResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/attachments/{AttachmentGuid}", "PUT")]
    [Route("/v1/attachments/{AttachmentGuid}", "PUT")]
    [DataContract]
    public partial class UpdateAttachment
        : IReturn<UpdateAttachmentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        [DataMember]
        public virtual string Type { get; set; }

        [DataMember]
        public virtual int? QrCodeXPosition { get; set; }

        [DataMember]
        public virtual int? QrCodeYPosition { get; set; }

        ///<summary>
        ///When passing QRCodeXPosition and QRCodeYPosition values, set this to true to save the values (or nulls).  Defaults to false. 
        ///</summary>
        [DataMember]
        public virtual bool? UseQrCodeCoordinates { get; set; }

        [DataMember]
        public virtual Guid? AttachmentGroupGuid { get; set; }

        ///<summary>
        ///When passing AttachmentGroupGuid values, set this to true to save the value (or null).  Defaults to false. 
        ///</summary>
        [DataMember]
        public virtual bool? UseAttachmentGroupGuid { get; set; }
    }

    public partial class UpdateAttachmentResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/campaigns/{CampaignGuid}", "PUT")]
    [DataContract]
    public partial class UpdateCampaign
        : IReturn<UpdateCampaignResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid CampaignGuid { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual string Description { get; set; }
    }

    [DataContract]
    public partial class UpdateCampaignResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/labels/{LabelGuid}", "PUT")]
    [DataContract]
    public partial class UpdateLabel
        : IReturn<UpdateLabelResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid LabelGuid { get; set; }

        ///<summary>
        ///The new label name.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual string Name { get; set; }
    }

    [Route("/v1/labels/{LabelGuid}/parent", "PUT")]
    [DataContract]
    public partial class UpdateLabelParent
        : IReturn<UpdateLabelParentResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid LabelGuid { get; set; }

        ///<summary>
        ///The new parent label. The parent is removed if the value is null.
        ///</summary>
        [DataMember]
        public virtual Guid? ParentLabelGuid { get; set; }
    }

    [DataContract]
    public partial class UpdateLabelParentResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public partial class UpdateLabelResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/notifications/heartbeat", "PUT")]
    [DataContract]
    public partial class UpdateNotificationHeartbeat
        : IReturn<UpdateNotificationHeartbeatResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid NotificationSessionToken { get; set; }
    }

    [DataContract]
    public partial class UpdateNotificationHeartbeatResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/user/updatepassword", "PUT")]
    [Route("/v1/user/updatepassword", "PUT")]
    [DataContract]
    public partial class UpdatePassword
        : IReturn<UpdatePasswordResponse>
    {
        [DataMember]
        public virtual string OldPassword { get; set; }

        [DataMember(IsRequired=true)]
        public virtual string NewPassword { get; set; }
    }

    [DataContract]
    public partial class UpdatePasswordResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/user/settings", "PUT")]
    [Route("/v1/user/settings", "PUT")]
    [DataContract]
    public partial class UpdateUserSettings
        : IReturn<UpdateUserSettingsResponse>
    {
        public UpdateUserSettings()
        {
            PushNotificationEvents = new List<string>{};
            InAppNotificationEvents = new List<string>{};
            EmailNotificationEvents = new List<string>{};
        }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string UserTitle { get; set; }

        [DataMember]
        public virtual string CompanyName { get; set; }

        [DataMember]
        public virtual string Language { get; set; }

        [DataMember]
        public virtual bool? AutoRetrieveMessage { get; set; }

        [DataMember]
        public virtual bool? OutOfOfficeEnabled { get; set; }

        [DataMember]
        public virtual string OutOfOfficeMessage { get; set; }

        [DataMember]
        public virtual List<string> PushNotificationEvents { get; set; }

        [DataMember]
        public virtual List<string> InAppNotificationEvents { get; set; }

        [DataMember]
        public virtual List<string> EmailNotificationEvents { get; set; }

        [DataMember]
        public virtual bool? EnableWebApp { get; set; }

        [DataMember]
        public virtual string SignatureHtml { get; set; }

        [DataMember]
        public virtual bool? IncludeSignatureOnReplyForward { get; set; }
    }

    [DataContract]
    public partial class UpdateUserSettingsResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/attachments/{AttachmentGuid}/chunk/{ChunkNumber}", "POST")]
    [Route("/v1/attachments/{AttachmentGuid}/chunk/{ChunkNumber}", "POST")]
    [DataContract]
    public partial class UploadAttachmentChunk
        : IReturn<UploadAttachmentChunkResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid AttachmentGuid { get; set; }

        ///<summary>
        ///The sequence number of the attachment Chunk.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual int ChunkNumber { get; set; }
    }

    public partial class UploadAttachmentChunkResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/v1/hash/{HashToken}/chunk/{ChunkNumber}", "POST")]
    [DataContract]
    public partial class UploadHashCalculationChunk
        : IReturn<UploadHashCalculationChunkResponse>
    {
        [DataMember(IsRequired=true)]
        public virtual Guid HashToken { get; set; }

        ///<summary>
        ///The sequence number of the attachment Chunk.
        ///</summary>
        [DataMember(IsRequired=true)]
        public virtual int ChunkNumber { get; set; }
    }

    public partial class UploadHashCalculationChunkResponse
    {
        [DataMember]
        public virtual ResponseStatus ResponseStatus { get; set; }
    }
}


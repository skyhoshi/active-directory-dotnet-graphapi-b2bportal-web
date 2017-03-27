﻿using AzureB2BInvite.Models;
using B2BPortal.Data;
using B2BPortal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureB2BInvite.Rules
{
    public static class GuestRequestRules
    {
        public static async Task<GuestRequest> SignUpAsync(GuestRequest request)
        {
            request.Init();
            var doc = await DocDBRepo.DB<GuestRequest>.CreateItemAsync(request);
            
            if (request.PreAuthed)
            {
                var domain = request.EmailAddress.Split('@')[1];

                //check to see if this domain has been approved for pre-authentication
                var approvedDomainSettings = (await DocDBRepo.DB<PreAuthDomain>.GetItemsAsync(d => d.DomainName == domain)).SingleOrDefault();
                if (approvedDomainSettings != null)
                {
                    request = await ExecuteDispositionAsync(request, approvedDomainSettings.AuthUser, approvedDomainSettings);
                }
            }

            return request;
        }

        public static async Task<GuestRequest> ExecuteDispositionAsync(GuestRequest request, string approver, PreAuthDomain domainSettings = null)
        {
            request.AuthUser = approver;
            request.LastModDate = DateTime.UtcNow;
           
            if (request.Disposition == Disposition.Approved || request.Disposition == Disposition.AutoApproved)
            {
                //INVITE
                request.Status = await InviteManager.SendInvitation(request, domainSettings);
            }

            //UPDATE
            await DocDBRepo.DB<GuestRequest>.UpdateItemAsync(request);
            return request;
        }

        public static async Task<PreAuthDomain> GetPreauthDomain(string domainName)
        {
            return (await DocDBRepo.DB<PreAuthDomain>.GetItemsAsync(d => d.DomainName == domainName)).SingleOrDefault();
        }

        public static async Task<GuestRequest> GetUserAsync(string email)
        {
            return (await DocDBRepo.DB<GuestRequest>.GetItemsAsync(r => r.EmailAddress == email)).SingleOrDefault();
        }

        public static async Task<IEnumerable<GuestRequest>> GetPendingRequestsAsync()
        {
            return await DocDBRepo.DB<GuestRequest>.GetItemsAsync(r => r.Disposition == Disposition.Pending);
        }
    }
}
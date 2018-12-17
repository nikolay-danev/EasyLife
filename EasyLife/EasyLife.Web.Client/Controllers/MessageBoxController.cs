using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.GlobalConstants;
using EasyLife.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLife.Web.Client.Controllers
{
	[Authorize(Roles = RoleType.Administrator)]
    public class MessageBoxController : Controller
    {
	    private readonly IMessageManager _messageaManager;
	    private readonly IMapper _mapper;

	    public MessageBoxController(IMessageManager messageaManager, IMapper mapper)
	    {
		    _messageaManager = messageaManager;
		    _mapper = mapper;
	    }

        public async Task<IActionResult> Index()
        {
	        var messages = await this._messageaManager.GetMessages(this.User.Identity.Name);

	        var messagesViewModels = this._mapper.Map<List<MessageViewModel>>(messages.Where(x => x.Sender.Email != this.User.Identity.Name).GroupBy(x => x.Sender.Email).Select(y => y.Last()).OrderByDescending(x => x.CreatedOn));

            return View(messagesViewModels);
        }

	    public async Task<IActionResult> OpenChat(int id)
	    {
		    var message = await _messageaManager.GetMessageInfo(id);

			var messages = await this._messageaManager.GetMessages(this.User.Identity.Name);

			var messagesViewModels = this._mapper.Map<List<MessageViewModel>>(messages.Where(x => (x.Sender.Email == message.Sender.Email && x.ReceiverEmail == User.Identity.Name) ||
			                                                                                      (x.Sender.Email == User.Identity.Name && x.ReceiverEmail == message.Sender.Email)).OrderBy(x => x.CreatedOn));

			var messageModel = _mapper.Map<List<MessageViewModel>>(messagesViewModels);

		    return this.View(messageModel);
	    }
    }
}
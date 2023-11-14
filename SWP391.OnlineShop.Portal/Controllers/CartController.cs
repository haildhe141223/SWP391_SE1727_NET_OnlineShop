using BraintreeHttp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayPal.Core;
using PayPal.v1.Identity;
using PayPal.v1.Payments;
using ServiceStack;
using SWP391.OnlineShop.Common.Helpers;
using SWP391.OnlineShop.Core.Models.Identities;
using SWP391.OnlineShop.ServiceInterface.Loggers;
using SWP391.OnlineShop.ServiceModel.ServiceModels;
using SWP391.OnlineShop.ServiceModel.ViewModels.Carts;
using System.Globalization;
using System.Net;
using System.Security.Claims;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.AddressModel;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.EmailModel;
using static SWP391.OnlineShop.ServiceModel.ServiceModels.OrderModels;

namespace SWP391.OnlineShop.Portal.Controllers
{
	public class CartController : BaseController
	{
		private readonly IJsonServiceClient _client;
		private readonly ILoggerService _logger;
		private readonly UserManager<User> _userManager;
		private static readonly Dictionary<string, int> PaypalData = new();

		public CartController(
		   IJsonServiceClient client,
		   ILoggerService logger,
		   UserManager<User> userManager)
		{
			_client = client;
			_logger = logger;
			_userManager = userManager;
		}

		public async Task<IActionResult> MyOrders()
		{
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			if (string.IsNullOrEmpty(email))
			{
				return RedirectToAction("Login", "Account");
			}
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var orders = await _client.GetAsync(new GetAllOrderByUser()
			{
				Email = email
			});
			return View(orders);
		}

		public async Task<IActionResult> Index()
		{
			var result = new List<OrderViewModels>();
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			if (string.IsNullOrEmpty(email))
			{
				return RedirectToAction("Login", "Account");
			}
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var productSlider = await _client.GetAsync(new GetAllProduct());
			TempData["slider"] = productSlider;
			var cartDetailOrders = await _client.GetAsync(new GetCartDetailByUser
			{
				Email = email
			});
			var cartContactOrders = await _client.GetAsync(new GetCartContactByUser
			{
				Email = email
			});
			var cartCompleteOrders = await _client.GetAsync(new GetCartCompletionByUser
			{
				Email = email
			});

			// get address by userEmail
			var userAddress = await _client.GetAsync(new GetAddressByUser()
			{
				Email = email
			});
			
			var province = await _client.GetAsync(new GetAllProvince());
			if (cartDetailOrders != null)
			{
				foreach (var item in cartDetailOrders)
				{
					if(item.OrderDetails != null)
					{
						item.CustomerAddress = userAddress.FullAddress;
						item.AddressId = userAddress.Id;
						item.CustomerPhoneNumber = user.PhoneNumber;
						item.Provinces = province;
						result.Add(item);
					}
				}
			}
			if (cartContactOrders != null)
			{
				foreach (var item in cartContactOrders)
				{
					if (item.OrderDetails != null)
					{
						item.CustomerAddress = userAddress.FullAddress;
						item.AddressId = userAddress.Id;
						item.CustomerPhoneNumber = user.PhoneNumber;
						item.Provinces = province;
						result.Add(item);
					}
				}
			}
			if (cartCompleteOrders != null)
			{
				foreach (var item in cartCompleteOrders)
				{
					if (item.OrderDetails != null)
					{
						item.CustomerAddress = userAddress.FullAddress;
						item.AddressId = userAddress.Id;
						item.CustomerPhoneNumber = user.PhoneNumber;
						item.Provinces = province;
						result.Add(item);
					}
				}
			}
			return View(result);
		}

		public async Task<IActionResult> CartContact(int id)
		{
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			if (string.IsNullOrEmpty(email))
			{
				return RedirectToAction("Login", "Account");
			}
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var productSlider = await _client.GetAsync(new GetAllProduct());
			var cartDetailOrders = await _client.GetAsync(new GetCartInfo
			{
				Id = id
			});
			var userAddress = await _client.GetAsync(new GetAddressByUser()
			{
				Email = email
			});
			cartDetailOrders.CustomerAddress = userAddress.FullAddress;
			cartDetailOrders.AddressId = userAddress.Id;
			cartDetailOrders.CustomerPhoneNumber = user.PhoneNumber;
			var province = await _client.GetAsync(new GetAllProvince());
			cartDetailOrders.Provinces = province;
			cartDetailOrders.Sliders = productSlider.Take(8).ToList();
			return View(cartDetailOrders);
		}

		public async Task<IActionResult> Checkout(int id)
		{
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var cartCompleteOrders = await _client.GetAsync(new GetCartInfo { Id = id });
			if (cartCompleteOrders?.OrderDetails == null || cartCompleteOrders.OrderDetails.Count <= 0)
			{
				return RedirectToAction("Index", "Cart");
			}
			var productSlider = await _client.GetAsync(new GetAllProduct());
			var userAddress = await _client.GetAsync(new GetAddressByUser()
			{
				Email = email
			});
			cartCompleteOrders.CustomerName = user.UserName;
			cartCompleteOrders.CustomerAddress = userAddress.FullAddress;
			cartCompleteOrders.AddressId = userAddress.Id;
			cartCompleteOrders.CustomerPhoneNumber = user.PhoneNumber;
			cartCompleteOrders.CustomerEmail = email;
			cartCompleteOrders.Sliders = productSlider.Take(8).ToList();
			return View(cartCompleteOrders);
		}

		public async Task<IActionResult> OrderInformation(int id)
		{
			var order = await _client.GetAsync(new GetCartInfo()
			{
				Id = id
			});
			var productSlider = await _client.GetAsync(new GetAllProduct());
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			if (string.IsNullOrEmpty(email))
			{
				return RedirectToAction("Login", "Account");
			}
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}
			order.CustomerPhoneNumber = user.PhoneNumber;

			if (string.IsNullOrEmpty(order.CustomerAddress))
			{
				var userAddress = await _client.GetAsync(new GetAddressByUser()
				{
					Email = email
				});
				order.CustomerAddress = userAddress.FullAddress;
			}
			if (string.IsNullOrEmpty(order.CustomerEmail))
			{

				order.CustomerAddress = email;
			}
			order.Sliders = productSlider.Take(8).ToList();
			return View(order);
		}

		public async Task<IActionResult> Confirmation([FromQuery(Name = "token")] string token, [FromQuery(Name = "payerId")] string payerId,
			[FromQuery(Name = "paymentId")] string paymentId)
		{
			var environment = new SandboxEnvironment(PaypalHelper.ClientId, PaypalHelper.SecretId);
			var client = new PayPalHttpClient(environment);
			var response = await ExecutePaymentAsync(client, payerId, paymentId);

			if (response.StatusCode != HttpStatusCode.OK) return RedirectToAction(nameof(PaymentFailed));

			if (string.IsNullOrEmpty(token))
			{
				return RedirectToAction("Index", "Home");
			}
			if (PaypalData.Keys.Contains(token))
			{
				var orderId = PaypalData[token];
				var order = await _client.GetAsync(new GetCartInfo()
				{
					Id = orderId
				});
				foreach (var item in order.OrderDetails)
				{
					var product = await _client.GetAsync(new GetProductById()
					{
						ProductId = item.Product.Id
					});
					var quantity = product.Amount - item.Quantity;
					await _client.PutAsync(new PutUpdateProduct()
					{
						Amount = quantity,
						Id = product.Id
					});
				}
				var api = await _client.PutAsync(new PutUpdateCartStatus()
				{
					Id = orderId,
					OrderStatus = Core.Models.Enums.OrderStatus.WaitingShipperToDeliver,
				});
				var emailBody = "Your Order has been paid successfully and waiting for delivery";
				var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
				var user = await _userManager.FindByEmailAsync(email);
				if (user == null)
				{
					return RedirectToAction("Login", "Account");
				}

				await _client.PostAsync(new PostAddEmail()
				{
					Body = emailBody,
					Category = "Notification",
					Subject = "Order Paid Successfully",
					To = email,
					Title = "Order Paid Successfully"
				});
				var productSlider = await _client.GetAsync(new GetAllProduct());
				order.Sliders = productSlider.Take(8).ToList();
				if (api.StatusCode == Common.Enums.StatusCode.Success)
				{
					return View(order);
				}
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> DeleteOrderDetail(int id)
		{
			var result = await _client.DeleteAsync(new DeleteOrderDetail()
			{
				Id = id
			});
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateQuantity(int id, int quantity)
		{
			var api = await _client.PutAsync(new PutUpdateQuantity()
			{
				Id = id,
				Quantity = quantity
			});
			return Ok(api);
		}

		[HttpPost]
		public async Task<IActionResult> ProcessToCheckout(int orderId)
		{
			var order = await _client.GetAsync(new GetCartInfo
			{
				Id = orderId
			});

			if (order is null)
			{
				return NotFound();
			}

			var total = 0m;

			foreach (var item in order.OrderDetails)
			{
				if (item.Status == Core.Models.Enums.Status.Active)
				{
					total += item.Quantity * item.UnitPrice;
				}
			}

			var api = await _client.PutAsync(new PutUpdateCartToContact()
			{
				Id = orderId,
				OrderStatus = Core.Models.Enums.OrderStatus.InCartContact,
				TotalCost = total
			});

			if (api.StatusCode == Common.Enums.StatusCode.Success)
			{
				return Ok(api);
			}

			return RedirectToAction("Error", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCart(int orderId, decimal total)
		{
			var api = await _client.PutAsync(new PutUpdateCartToContact
			{
				Id = orderId,
				OrderStatus = Core.Models.Enums.OrderStatus.InCartDetail,
				TotalCost = total
			});
			if (api.StatusCode == Common.Enums.StatusCode.Success)
			{
				return Ok(api);
			}
			return RedirectToAction("Error", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> CancelCart(int orderId, decimal total)
		{
			var api = await _client.PutAsync(new PutUpdateCartToContact
			{
				Id = orderId,
				OrderStatus = Core.Models.Enums.OrderStatus.Canceled,
				TotalCost = total
			});
			if (api.StatusCode == Common.Enums.StatusCode.Success)
			{
				return Ok(api);
			}
			return RedirectToAction("Error", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCartToComplete(int orderId, decimal total, string address)
		{
			var api = await _client.PutAsync(new PutUpdateCartToContact
			{
				Id = orderId,
				OrderStatus = Core.Models.Enums.OrderStatus.InCartCompletion,
				TotalCost = total,
				Address = address
			});
			if (api.StatusCode == Common.Enums.StatusCode.Success)
			{
				return Ok(api);
			}
			return RedirectToAction("Error", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> FinishCart(int orderId, decimal total, string note)
		{
			var api = await _client.PutAsync(new PutUpdateCartToContact
			{
				Id = orderId,
				OrderStatus = Core.Models.Enums.OrderStatus.WaitingShipperToDeliver,
				TotalCost = total,
				OrderNotes = note
			});
			if (api.StatusCode == Common.Enums.StatusCode.Success)
			{
				var order = await _client.GetAsync(new GetCartInfo()
				{
					Id = orderId
				});
				foreach (var item in order.OrderDetails)
				{
					var product = await _client.GetAsync(new GetProductById()
					{
						ProductId = item.Product.Id
					});
					var quantity = product.Amount - item.Quantity;
					await _client.PutAsync(new PutUpdateProduct()
					{
						Amount = quantity,
						Id = product.Id
					});
				}
				return Ok(api);
			}
			return RedirectToAction("Error", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> AddToCard(int productId, decimal price, int quantity, int sizeId)
		{
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var userAddress = await _client.GetAsync(new GetAddressByUser
			{
				Email = email
			});
			var product = await _client.GetAsync(new GetProductById
			{
				ProductId = productId
			});
			if (product.Amount < quantity)
			{
				return StatusCode(500, $"There's not enough product in store. Current in store {product.Amount}");
			}
			var api = await _client.PostAsync(new PostAddToCart
			{
				CustomerEmail = email,
				CustomerAddress = userAddress.FullAddress,
				CustomerName = user.NormalizedUserName,
				OrderStatus = Core.Models.Enums.OrderStatus.InCartDetail,
				Quantity = quantity,
				Price = price,
				ProductId = productId
			});
			if(api.StatusCode == Common.Enums.StatusCode.Success)
			{
			return Ok();
			}
			else
			{
				if (!string.IsNullOrEmpty(api.ErrorMessage))
				{
					return StatusCode(500,api.ErrorMessage);
				}
				else
				{
					return StatusCode(500, "There's not enough product in store");
				}
			}
		}

		#region Paypal Payment
		[HttpPost]
		public async Task<IActionResult> MakePaypalPayment(string data, string notes)
		{
			if (string.IsNullOrEmpty(data))
			{
				return StatusCode(500, "Empty Data");
			}
			var model = JsonConvert.DeserializeObject<OrderViewModels>(data);
			if (model is null)
			{
				return StatusCode(500, "Empty Data");
			}
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return RedirectToAction("ErrorForbidden", "Account");
			}
			var environment = new SandboxEnvironment(PaypalHelper.ClientId, PaypalHelper.SecretId);
			var client = new PayPalHttpClient(environment);
			#region Create Paypal Order

			var itemList = new ItemList()
			{
				Items = new List<Item>()
			};

			foreach (var order in model.OrderDetails)
			{
				var item = new Item()
				{
					Currency = "USD",
					Name = order.Product.ProductName,
					Price = order.UnitPrice.ToString(CultureInfo.InvariantCulture),
					Quantity = order.Quantity.ToString(),
					Sku = "sku",
					Tax = "0"
				};
				itemList.Items.Add(item);
			}
			var paypalOrderId = Guid.NewGuid();
			var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
			var payment = new Payment()
			{
				Intent = "sale",
				Transactions = new List<Transaction>()
				{
					new()
					{
						Amount = new Amount()
						{
							Total = model.TotalCost.ToString(CultureInfo.InvariantCulture),
							Currency = "USD",
							Details = new AmountDetails
							{
								Tax = "0",
								Shipping = "0",
								Subtotal = model.TotalCost.ToString(CultureInfo.InvariantCulture)
							}
						},
						ItemList = itemList,
						Description = $"Invoice #{paypalOrderId}",
						InvoiceNumber = paypalOrderId.ToString()
					}
				},
				RedirectUrls = new RedirectUrls()
				{
					CancelUrl = $"{hostname}/Cart/PaymentFailed",
					ReturnUrl = $"{hostname}/Cart/Confirmation"
				},
				Payer = new Payer()
				{
					PaymentMethod = "paypal"
				}
			};
			var request = new PaymentCreateRequest();
			request.RequestBody(payment);
			var paypalDirectUrl = await GetPaypalDirectUrl(client, request);
			if (!string.IsNullOrEmpty(notes))
			{
				await _client.PutAsync(new PutUpdateOrderNotes()
				{
					Id = model.Id,
					OrderNotes = notes
				});
			}

			#endregion

			PaypalData.Add(paypalDirectUrl.Split("&token=")[1], model.Id);
			return Ok(paypalDirectUrl);
		}

		public async Task<IActionResult> PaymentFailed()
		{
			var emailBody = "Your Order has been paid failed! Please try again or choose another payment method";
			var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}
			await _client.PostAsync(new PostAddEmail()
			{
				Body = emailBody,
				Category = "Notification",
				Subject = "Order Paid Failed",
				To = email,
				Title = "Order Paid Failed"
			});
			return View();
		}

		public async Task<string> GetPaypalDirectUrl(PayPalHttpClient client, PaymentCreateRequest request)
		{
			try
			{
				var response = await client.Execute(request);
				var result = response.Result<Payment>();

				using var links = result.Links.GetEnumerator();
				var paypalRedirectUrl = string.Empty;
				while (links.MoveNext())
				{
					if (links.Current == null) continue;

					var link = links.Current;
					if (link.Rel.ToLower().Trim().Equals("approval_url"))
					{
						//saving the paypal redirect URL to which user will be redirected for payment  
						paypalRedirectUrl = link.Href;
					}
				}

				return paypalRedirectUrl;
			}
			catch (HttpException httpException)
			{
				var statusCode = httpException.StatusCode;
				var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

				//Process when Checkout with Paypal fails
				_logger.LogError($"Error in GetPaypalDirectUrl - {statusCode} - {debugId}");

				return "/Cart/PaymentFail";
			}
		}

		public async Task<BraintreeHttp.HttpResponse> ExecutePaymentAsync(PayPalHttpClient http, string payerId, string paymentId)
		{
			var paymentExecution = new PaymentExecution()
			{
				PayerId = payerId
			};
			var request = new PaymentExecuteRequest(paymentId);
			request.RequestBody(paymentExecution);
			var response = await http.Execute(request);
			return response;
		}
		#endregion

		#region Address

		public async Task<IActionResult> GetDistrictByProvince(int provinceId)
		{
			var data = await _client.GetAsync(new GetDistrictByProvince()
			{
				ProvinceId = provinceId
			});
			return Ok(data);
		}

		public async Task<IActionResult> GetWardByDistrict(int districtId)
		{
			var data = await _client.GetAsync(new GetWardByDistrict()
			{
				DistrictId = districtId
			});
			return Ok(data);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAddress(int id, string fullAddress)
		{
			await _client.PutAsync(new PutUpdateAddress
			{
				Id = id,
				FullAddress = fullAddress
			});
			return Ok();
		}
		#endregion
	}
}

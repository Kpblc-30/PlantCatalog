global using PlantCatalog.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PlantCatalog.Components.Services;
public class CartService
{
    private readonly IProductService _productService;
    private readonly List<CartItem> _cartItems = new();
    private const int MaxItems = 5;
    public event Action? OnCartChanged;
    public CartService(IProductService productService)
    {
        _productService = productService;
    }
    public List<CartItem> GetCartItems()
    {
        return _cartItems;
    }
    public async Task AddToCartAsync(int productId)
    {
        if (_cartItems.Count >= MaxItems)
        {
            throw new InvalidOperationException($"Лимит корзины превышен! Максимум {MaxItems} товара.");
        }
        var product = await _productService.GetByIdAsync(productId);
        var existingItem = _cartItems.FirstOrDefault(i => i.ProductId == productId);
        
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            _cartItems.Add(new CartItem { ProductId = productId, Product = product, Quantity = 1 });
        }
        OnCartChanged?.Invoke();
    }
    public void RemoveFromCart(int productId)
    {
        var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                _cartItems.Remove(item);
            }
        }
        OnCartChanged?.Invoke();
    }
    public void ClearCart()
    {
        _cartItems.Clear();
        OnCartChanged?.Invoke();
    }
    public decimal GetTotalPrice()
    {
        return _cartItems.Sum(i => i.Product?.Price * i.Quantity ?? 0);
    }
}
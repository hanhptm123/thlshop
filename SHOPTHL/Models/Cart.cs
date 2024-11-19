using SHOPTHL.Data;

namespace SHOPTHL.Models
{
	public class Cart
	{
		public List<CartLine> Lines {  get; set; } = new List<CartLine>();
		public void AddItem(Sanpham sanpham,int quantity)
		{
			
			CartLine? line = Lines
			.Where(s => s.Sanpham.Masp == sanpham.Masp)
			.FirstOrDefault();
			if (line == null)
			{
				Lines.Add(line = new CartLine
				{
					Sanpham = sanpham,
					Quantity = quantity
				});
			}
			else
			{
				line.Quantity += quantity;
			}

		}
		public void RemoveLine(Sanpham sanpham) =>
			Lines.RemoveAll(l => l.Sanpham.Masp == sanpham.Masp);
		public decimal ComputeTotalValue()
		{
			return (decimal)Lines.Sum(e => e.Sanpham?.Dongia * e.Quantity); ;
		}
		public void Clear() => Lines.Clear();
	}
	public class CartLine
	{
		public int CartLineID { get; set; }
		public Sanpham Sanpham { get; set; } = new();
		public int Quantity { get; set; }
	}

}

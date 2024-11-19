using SHOPTHL.Data;

namespace SHOPTHL.Models
{
	public class Like
	{
		public List<LikeLine> Lines {  get; set; } = new List<LikeLine>();
		public void AddItem(Sanpham sanpham,int quantity)
		{

            LikeLine? line = Lines
			.Where(s => s.Sanpham.Masp == sanpham.Masp)
			.FirstOrDefault();
			if (line == null)
			{
				Lines.Add(line = new LikeLine
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
	public class LikeLine
    {
		public int CartLineID { get; set; }
		public Sanpham Sanpham { get; set; } = new();
		public int Quantity { get; set; }
	}

}

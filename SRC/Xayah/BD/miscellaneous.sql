--delete from BankTransfer;


select b.Date, b.Value,b.Type, b.FileName, Count(*) from BankTransfer b
group by b.Date, b.Value, b.Type, b.FileName
 order by b.Date desc
alter table [dbo].[Customer]
add constraint Address_FK FOREIGN KEY (AddressId) references Address(Id)
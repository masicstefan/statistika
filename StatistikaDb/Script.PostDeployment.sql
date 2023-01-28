if not exists (select 1 from dbo.tabStatistika)
begin
	insert into dbo.tabStatistika (DatumVpisa, ImeKlicaneStoritve)
	values ('11.1.2023', 'GET narocilnica'), ('11.1.2023', 'GET zahtevek');
end
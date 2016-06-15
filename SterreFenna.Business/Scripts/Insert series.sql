INSERT INTO [dbo].[Gallery](Name, Created, Published)
VALUES
('Teeez I', GETDATE(), GETDATE()), 
('Teeez II', GETDATE(), GETDATE()), 
('Anke van den Ban', GETDATE(), GETDATE())

INSERT INTO [dbo].[GalleryItem] (Location, Created, Gallery_Id)
VALUES 
('/Galleries/Teez_I/01_b.jpg', GETDATE(), 1)
,('/Galleries/Teez_I/02.jpg', GETDATE(), 1)
,('/Galleries/Teez_I/03.jpg', GETDATE(), 1)
,('/Galleries/Teez_I/04.jpg', GETDATE(), 1)
,('/Galleries/Teez_I/04b.jpg', GETDATE(), 1)
,('/Galleries/Teez_I/05.jpg', GETDATE(), 1)
,('/Galleries/Teez_I/06.jpg', GETDATE(), 1)
,('/Galleries/Teez_I/07.jpg', GETDATE(), 1)
,('/Galleries/Teez_I/08.jpg', GETDATE(), 1)
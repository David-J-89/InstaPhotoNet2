using InstaPhotoNet.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaPhotoNet.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public IQueryable<Comment> GetCommentsByPhoto(int photoId)
        {
            var commentsbphoto = _context.Comments.Where(r => r.PhotoId == photoId);
            return commentsbphoto;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            var photos = await _context.Photos.Include(c => c.Comments).ToListAsync();

            return photos;

            //var photos = await _context.Photos.Include(u => u.User).ToListAsync();            

            //return photos;       

        }

        public async Task<IEnumerable<Photo>> GetPhotosIncludingComments()
        {
            var photosic = await _context.Photos.Include(c => c.Comments).ToListAsync();
            return photosic;
        }

        //public Task<IQueryable<Photo>> GetPhotosIncludingComments()
        //{
        //    throw new System.NotImplementedException();
        //}

        public async Task<Photo> GetProfilePhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId)
                .FirstOrDefaultAsync(p => p.IsProfile);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        //Task<IEnumerable<Comment>> IPostRepository.GetCommentsByPhoto(int photoId)
        //{
        //    var commentrep = _context.Photos.Where(r => r.Id == photoId).ToListAsync();
        //    return commentrep;
        //}

        //IQueryable<Photo> IPostRepository.GetPhotosIncludingComments()
        //{
        //    var commentsbphoto = _context.Photos.Include("Comments");
        //    return commentsbphoto;
        //}
    }
}

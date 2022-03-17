using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Messenger.DB.entity
{
    public partial class MessengerContext : DbContext
    {
        public MessengerContext()
        {
        }

        public MessengerContext(DbContextOptions<MessengerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bot> Bots { get; set; }
        public virtual DbSet<Call> Calls { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Dialog> Dialogs { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageSticker> MessageStickers { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<Sticker> Stickers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAccess> UserAccesses { get; set; }
        public virtual DbSet<Value> Values { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=messenger;Uid=root;Pwd=Stepup007;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bot>(entity =>
            {
                entity.ToTable("bot");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.DescriptionBot)
                    .IsRequired()
                    .HasColumnName("description_bot");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Bot)
                    .HasPrincipalKey<User>(p => p.BotId)
                    .HasForeignKey<Bot>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bot_ibfk_1");
            });

            modelBuilder.Entity<Call>(entity =>
            {
                entity.ToTable("calls");

                entity.HasIndex(e => e.DialogId, "dialog_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Continuously).HasColumnName("continuously");

                entity.Property(e => e.DialogId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("dialog_id");

                entity.Property(e => e.RecordRef)
                    .HasMaxLength(255)
                    .HasColumnName("record_ref");

                entity.HasOne(d => d.Dialog)
                    .WithMany(p => p.Calls)
                    .HasForeignKey(d => d.DialogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("calls_ibfk_1");
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.ToTable("command");

                entity.HasIndex(e => e.IdBot, "id_bot");

                entity.HasIndex(e => e.NameCommand, "name_com");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.IdBot)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id_bot");

                entity.Property(e => e.NameCommand)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("name_command");

                entity.HasOne(d => d.IdBotNavigation)
                    .WithMany(p => p.Commands)
                    .HasForeignKey(d => d.IdBot)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("command_ibfk_1");
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.ToTable("content");

                entity.HasIndex(e => e.MessageId, "content_ibfk_1");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.MessageId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("message_id");

                entity.Property(e => e.RefToContent)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("ref_to_content");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("content_ibfk_1");
            });

            modelBuilder.Entity<Dialog>(entity =>
            {
                entity.ToTable("dialog");

                entity.HasIndex(e => e.IdAdmin, "id_admin");

                entity.HasIndex(e => e.Name, "name_dialog");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.IdAdmin)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id_admin");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("name");

                entity.Property(e => e.RefAvatar)
                    .HasMaxLength(40)
                    .HasColumnName("ref_avatar");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Dialogs)
                    .HasForeignKey(d => d.IdAdmin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dialog_ibfk_1");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.HasIndex(e => e.SendDate, "date_message");

                entity.HasIndex(e => e.UserAccessId, "fk1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.SendDate).HasColumnName("send_date");

                entity.Property(e => e.TextMessage)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("text_message");

                entity.Property(e => e.UserAccessId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("user_access_id");

                entity.HasOne(d => d.UserAccess)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserAccessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk1");
            });

            modelBuilder.Entity<MessageSticker>(entity =>
            {
                entity.HasKey(e => new { e.MessageId, e.StickerId })
                    .HasName("PRIMARY");

                entity.ToTable("message_stickers");

                entity.HasIndex(e => e.MessageId, "message_id")
                    .IsUnique();

                entity.HasIndex(e => e.StickerId, "message_stickers_ibfk_2");

                entity.Property(e => e.MessageId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("message_id");

                entity.Property(e => e.StickerId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("sticker_id");

                entity.HasOne(d => d.Message)
                    .WithOne(p => p.MessageSticker)
                    .HasForeignKey<MessageSticker>(d => d.MessageId)
                    .HasConstraintName("message_stickers_ibfk_1");

                entity.HasOne(d => d.Sticker)
                    .WithMany(p => p.MessageStickers)
                    .HasForeignKey(d => d.StickerId)
                    .HasConstraintName("message_stickers_ibfk_2");
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.ToTable("parameter");

                entity.HasIndex(e => e.Name, "name_param");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Sticker>(entity =>
            {
                entity.ToTable("stickers");

                entity.HasIndex(e => e.TextSticker, "text_hint");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.RefImage)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("ref_image");

                entity.Property(e => e.RefPack)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("ref_pack");

                entity.Property(e => e.TextSticker)
                    .HasMaxLength(40)
                    .HasColumnName("text_sticker");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.BotId, "bot_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("avatar")
                    .HasDefaultValueSql("'vk.com/avatar0'");

                entity.Property(e => e.BotId)
                    .IsRequired()
                    .HasColumnType("int unsigned")
                    .HasColumnName("bot_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .HasColumnName("password");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'Нет'");
            });

            modelBuilder.Entity<UserAccess>(entity =>
            {
                entity.ToTable("user_access");

                entity.HasIndex(e => e.DialogId, "fk_dialog_id_idx");

                entity.HasIndex(e => e.UserId, "fk_user_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.DialogId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("dialog_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Dialog)
                    .WithMany(p => p.UserAccesses)
                    .HasForeignKey(d => d.DialogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_dialog_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAccesses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_id");
            });

            modelBuilder.Entity<Value>(entity =>
            {
                entity.ToTable("value");

                entity.HasIndex(e => new { e.IntValue, e.StringValue }, "data_value");

                entity.HasIndex(e => e.IdUser, "id_user");

                entity.HasIndex(e => e.ParameterId, "parametr_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.IdUser)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id_user");

                entity.Property(e => e.IntValue)
                    .HasColumnType("int unsigned")
                    .HasColumnName("int_value");

                entity.Property(e => e.ParameterId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("parameter_id");

                entity.Property(e => e.StringValue)
                    .HasMaxLength(100)
                    .HasColumnName("string_value");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Values)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("value_ibfk_1");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.Values)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("value_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

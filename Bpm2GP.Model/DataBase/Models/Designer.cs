﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Bpm2GP.Model.DataBase.Models
{
    public class Designer
    {
        public virtual Guid Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String ShortBio { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Project>  Projects { get; set; }
        public virtual IList<DesignTeam> Teams { get; set; }

        public Designer()
        {
            this.Projects = new List<Project>();    
            this.Teams = new List<DesignTeam>();
        }
    }

    public class DesignerMap : ClassMapping<Designer>
    {
        public DesignerMap()
        {
            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Guid);
            });

            Property(x => x.Name);
            Property(x => x.ShortBio);

            OneToOne(x => x.User, map =>
            {
                map.PropertyReference(typeof (User).GetProperty("Designer"));
                map.Lazy(LazyRelation.Proxy);
                map.Cascade(Cascade.All);
            });

            Bag<Project>(x => x.Projects, m =>
            {
                m.Key(k => k.Column("idDesigner"));
                m.Inverse(true);
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.NoLazy);
            }, o => o.OneToMany());

            Bag<DesignTeam>(x => x.Teams, map =>
            {
                map.Cascade(Cascade.All);
                map.Inverse(true);
                map.Lazy(CollectionLazy.NoLazy);
                map.Key(k => k.Column("idDesigner"));
            }, o => o.OneToMany());
        }
    }
}

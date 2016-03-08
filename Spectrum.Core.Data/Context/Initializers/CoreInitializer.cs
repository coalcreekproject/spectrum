using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Context.Initializers
{
    internal class CoreInitializer : CreateDatabaseIfNotExists<CoreDbContext>
    {
        //TODO: Should we break this into partials somehow?  Getting into 500 lines here   
        protected override void Seed(CoreDbContext context)
        {
            var hasher = new PasswordHasher();

            #region Oranization Type Seeding

            new List<OrganizationType>
            {
                new OrganizationType
                {
                    Id = 1,
                    Name = "Local Government",
                    Description =
                        "Municipality, city, town, township or bourough that has icorporated status and local government"
                },
                new OrganizationType
                {
                    Id = 2,
                    Name = "County Government",
                    Description = "County governments"
                },
                new OrganizationType
                {
                    Id = 3,
                    Name = "State Government",
                    Description = "State governments"
                },
                new OrganizationType
                {
                    Id = 4,
                    Name = "Federal Government",
                    Description = "Federal government"
                },
                new OrganizationType
                {
                    Id = 5,
                    Name = "Non-government Organization",
                    Description = "Non-government organization (example: Red Cross)"
                },
                new OrganizationType
                {
                    Id = 6,
                    Name = "Hospitals and Clinics",
                    Description = "Hospitals"
                },
                new OrganizationType
                {
                    Id = 7,
                    Name = "Agency - First Responder",
                    Description = "First responder Agencies, Fire, EMS"
                },
                new OrganizationType
                {
                    Id = 8,
                    Name = "Agency - Law Enforcement",
                    Description = "LE Organizations, (example: City and State Police)"
                },
                new OrganizationType
                {
                    Id = 9,
                    Name = "Agency - Investigative",
                    Description = "Investigative Agencies"
                },
                new OrganizationType
                {
                    Id = 10,
                    Name = "Agency - Other",
                    Description = "Other Agencies"
                },
                new OrganizationType
                {
                    Id = 11,
                    Name = "Private",
                    Description = "Private companies and organizations"
                },
                new OrganizationType
                {
                    Id = 12,
                    Name = "Individual",
                    Description = "Private citizen or individual"
                }
            }.ForEach(t => context.OrganizationTypes.Add(t));

            #endregion

            #region Organization, Profile and Role Seeding

            var address = new Address
            {
                Name = "Corporate Office",
                Default = true,
                Description = "Home Office of Spectrum Operational LLC",
                StreetOne = "2770 Arapahoe Road",
                StreetTwo = "Suite 132-113",
                City = "Lafayette",
                State = "CO",
                Zip = "80026"
            };

            var organizationProfileAdresses = new List<OrganizationProfileAddress>();
            OrganizationProfileAddress organizationProfileAddress = new OrganizationProfileAddress {Address = address};
            organizationProfileAdresses.Add(organizationProfileAddress);

            var organizationProfiles = new List<OrganizationProfile>
            {
                new OrganizationProfile
                {
                    Default = true,
                    ProfileName = " Default - Spectrum Operational LLC",
                    Description = "Spectrum Operational HQ Profile",
                    PrimaryContact = "Developer",
                    Phone = "303-704-2500",
                    Email = "develop@spectrumoperational.com",
                    County = "Boulder",
                    Country = "United States",
                    TimeZone = "US Mountain",
                    DstAdjust = true,
                    Language = "US English",
                    OrganizationProfileAddresses = organizationProfileAdresses
                }
            };

            new List<Organization>
            {
                new Organization
                {
                    Name = "Spectrum Operational",
                    OrganizationTypeId = 11,
                    OrganizationProfiles = organizationProfiles
                }
            }.ForEach(o => context.Organizations.Add(o));

            context.SaveChanges();

            var organization = context.Organizations.FirstOrDefault(o => o.Name == "Spectrum Operational");

            if (organization != null)
            {
                organization.Roles.Add(new Role { Name = "superuser", Description = "God mode", OrganizationId = organization.Id });
                organization.Roles.Add(new Role {Name = "admin", Description = "Administrator", OrganizationId = organization.Id });
                organization.Roles.Add(new Role { Name = "user", Description = "Standard user", OrganizationId = organization.Id });
                organization.Roles.Add(new Role { Name = "observer", Description = "Read only role", OrganizationId = organization.Id });

                context.Organizations.AddOrUpdate(organization);
            }

            #endregion

            #region User and Profile seeding

            var superuser = new User
            {
                UserName = "superuser",
                Email = "superuser@spectrumoperational.com",
                PasswordHash = hasher.HashPassword("p@ssw0rd")
            };

            var develop = new User
            {
                UserName = "develop",
                Email = "develop@spectrumoperational.com",
                PasswordHash = hasher.HashPassword("p@ssw0rd")
            };

            var developProfile = new UserProfile
            {
                OrganizationId = context.Organizations.FirstOrDefault(org => org.Name == "Spectrum Operational").Id,
                Default = true,
                ProfileName = "Default for Developer, Spectrum Operational LLC",
                FirstName = "",
                MiddleName = "",
                LastName = "",
                Nickname = "",
                SecondaryEmail = "developer@example.net",
                SecondaryPhoneNumber = "720-555-1212",
                TimeZone = "US Mountain",
                DstAdjust = true
            };

            var superUserProfile = new UserProfile
            {
                OrganizationId = context.Organizations.FirstOrDefault(org => org.Name == "Spectrum Operational").Id,
                Default = true,
                ProfileName = "Default for superuser, Spectrum Operational LLC",
                FirstName = "System",
                MiddleName = "System",
                LastName = "System",
                Nickname = "System",
                SecondaryEmail = "support@spectrumoperational.com",
                //SecondaryPhoneNumber = "", for future assignment
                TimeZone = "US Mountain",
                DstAdjust = true
            };

            UserProfileAddress developUserProfileAddress = new UserProfileAddress {Address = address};
            developUserProfileAddress.UserProfile = developProfile;

            developProfile.UserProfileAddresses.Add(developUserProfileAddress);
            develop.UserProfiles.Add(developProfile);

            UserProfileAddress superuserUserProfileAddress = new UserProfileAddress {Address = address};
            superuserUserProfileAddress.UserProfile = superUserProfile;

            superUserProfile.UserProfileAddresses.Add(superuserUserProfileAddress);
            superuser.UserProfiles.Add(superUserProfile);

            context.Users.Add(develop);
            context.Users.Add(superuser);

            context.SaveChanges();

            #endregion

            #region User Organization Associations

            new List<UserOrganization>
            {
                new UserOrganization
                {
                    UserId = context.Users.FirstOrDefault(user => user.UserName == "superuser").Id,
                    OrganizationId =
                        context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new UserOrganization
                {
                    UserId = context.Users.FirstOrDefault(user => user.UserName == "develop").Id,
                    OrganizationId =
                        context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                }
            }.ForEach(uo => context.UserOrganizations.Add(uo));

            #endregion

            #region User Role Associations

            //Assign all available roles
            foreach (Role p in context.Roles)
            {
                UserRole ur = new UserRole()
                {
                    UserId = context.Users.FirstOrDefault(user => user.UserName == "develop").Id,
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id,
                    RoleId = p.Id
                };
                context.UserRoles.Add(ur);
            }

            //Assign one role to superuser
            new List<UserRole>
            {
                new UserRole
                {
                    UserId = context.Users.FirstOrDefault(user => user.UserName == "superuser").Id,
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id,
                    RoleId = context.Roles.FirstOrDefault(r => r.Name.Equals("superuser")).Id
                }
            }.ForEach(ur => context.UserRoles.Add(ur));

            context.SaveChanges();

            #endregion

            #region Positions ESF Seed

            new List<Position>
            {
                new Position
                {
                    Name = "ESF #1 - Transportation",
                    Description =
                        "Transportation provides support by assisting local, state, tribal, territorial, insular area, and " +
                        "Federal governmental entities, voluntary organizations, nongovernmental organizations, and the private" +
                        " sector in the management of transportation systems and infrastructure during domestic threats or in " +
                        "response to actual or potential incidents.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #2 - Communications",
                    Description =
                        "Communications supports the restoration of communications infrastructure, coordinates communications " +
                        "support to response efforts, facilitates the delivery of information to emergency management decision " +
                        "makers, and assists in the stabilization and reestablishment of systems and applications from cyber " +
                        "attacks during incidents.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #3 - Public Works and Engineering",
                    Description =
                        "Public Works and Engineering coordinates and organizes the resources of the Federal Government to " +
                        "facilitate the delivery of multiple core capabilities.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #4 - Firefighting",
                    Description =
                        "Firefighting provides Federal support for the detection and suppression of wildland, rural, and urban " +
                        "fires resulting from, or occurring coincidentally with, an all hazard incident requiring a coordinated" +
                        " national response for assistance.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #5 - Information and Planning",
                    Description =
                        "Information and Planning collects, analyzes, processes, and disseminates information about a potential" +
                        " or actual incident and conducts planning activities to facilitate the overall activities in providing " +
                        " assistance to the whole community.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #6 - Mass Care, Emergency Assistance, Temporary Housing, and Human Services",
                    Description =
                        "Mass Care, Emergency Assistance, Temporary Housing, and Human Services coordinates and provides " +
                        "life-sustaining resources, essential services, and statutory programs when the needs of disaster " +
                        "survivors exceed local, state, tribal, territorial, and insular area government capabilities. ",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #7 - Logistics",
                    Description =
                        "Logistics integrates whole community logistics incident planning and support for timely and efficient" +
                        " delivery of supplies, equipment, services, and facilities. It also facilitates comprehensive logistics" +
                        " planning, technical assistance, training, education, exercise, incident response, and sustainment that " +
                        "leverage the capability and resources of Federal logistics partners, public and private stakeholders, and" +
                        " nongovernmental organizations (NGOs) in support of both responders and disaster survivors.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #8 - Public Health and Medical Services",
                    Description =
                        "Public Health and Medical Services provides the mechanism for Federal assistance to supplement local, state," +
                        " tribal, territorial, and insular area resources in response to a disaster, emergency, or incident that may lead" +
                        " to a public health, medical, behavioral, or human service emergency, including those that have international " +
                        "implications.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #9 - Search and Rescue (SAR)",
                    Description =
                        "Search and Rescue (SAR) deploys Federal SAR resources to provide lifesaving assistance to local, state, tribal, " +
                        "territorial, and insular area authorities, including local SAR Coordinators and Mission Coordinators, when there " +
                        "is an actual or anticipated request for Federal SAR assistance.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #10 - Oil and Hazardous Materials",
                    Description =
                        "Oil and Hazardous Materials Response provides Federal support in response to an actual or potential discharge and/or" +
                        " release of oil or hazardous materials when activated.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #11 - Agriculture and Natural Resources",
                    Description =
                        "Agriculture and Natural Resources organizes and coordinates Federal support for the protection of the Nation’s " +
                        "agricultural and natural and cultural resources during national emergencies. ESF #11 works during actual and potential" +
                        " incidents to provide nutrition assistance; respond to animal and agricultural health issues; provide technical expertise," +
                        " coordination and support of animal and agricultural emergency management; ensure the safety and defense of the Nation’s" +
                        " supply of meat, poultry, and processed egg products; and ensure the protection of natural and cultural resources and " +
                        "historic properties.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #12 - Energy",
                    Description =
                        "Energy facilitates the reestablishment of damaged energy systems and components when activated by the Secretary of Homeland" +
                        " Security for incidents requiring a coordinated Federal response.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #13 - Public Safety and Security",
                    Description =
                        "Provides Federal public safety and security assistance to local, state, tribal, territorial, insular area, and Federal law " +
                        "enforcement organizations overwhelmed by the results of an actual or anticipated natural/manmade disaster or an act of terrorism.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #14 - Superseded by the National Disaster Recovery Framework",
                    Description =
                        "Superseded by the National Disaster Recovery Framework",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
                new Position
                {
                    Name = "ESF #15 - External Affairs",
                    Description =
                        "External Affairs provides accurate, coordinated, timely, and accessible information to affected audiences, including governments," +
                        " media, the private sector, and the local populace, including children, those with disabilities and others with access and " +
                        "functional needs, and individuals with limited English proficiency.",
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                },
            }.ForEach(esf => context.Positions.Add(esf));

            context.SaveChanges();

            #endregion

            #region UserPosition Associations

            foreach (Position p in context.Positions)
            {
                UserPosition up = new UserPosition()
                {
                    PositionId = p.Id,
                    UserId = context.Users.FirstOrDefault(user => user.UserName.Equals("develop")).Id,
                    OrganizationId = context.Organizations.FirstOrDefault(org => org.Name.Equals("Spectrum Operational")).Id
                };
                context.UserPositions.Add(up);
            }

            #endregion

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
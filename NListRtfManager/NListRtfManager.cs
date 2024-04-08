using LSSERVICEPROVIDERLib;
using Patholab_DAL_V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NListRtfManager
{
    public static class RtfManager
    {
        const string All_organs = "ALL";
        //private SDG sdg;
        //private DataLayer dal;

        public static IEnumerable<string> getNListByPathologId(SDG sdg, DataLayer dal, INautilusUser ntlsUser)
        {
            string sdgType = sdg.NAME[0].ToString();
            string operatorId = ntlsUser.GetOperatorId().ToString();
            //double roleId = ntlsUser.GetRoleId();

            //If has organ get it, else get "All
            List<string> organs4Sdg = sdg.SAMPLEs.Select(SM => SM.SAMPLE_USER.U_ORGAN ?? All_organs).Distinct().ToList();
            string allOrgans = string.Join(",", organs4Sdg.ToArray());
            //if (!organs4Sdg.Contains(All_organs))
            //    organs4Sdg.Add(All_organs);
            List<U_NLIST_USER> organsList;

            organsList = (from item in dal.GetAll<U_NLIST_USER>()
                          where item.U_SDG_TYPE == sdgType && (item.U_OPERATORS_ID.Contains(operatorId) || item.U_OPERATORS_ID.Contains(All_organs)) &&
                          (allOrgans.Contains(item.U_ORGANS) || allOrgans.Contains(allOrgans)) select item).ToList();

            //if (allOrgans.Contains(All_organs))
            //{
            //    organsList = (from item in dal.GetAll<U_NLIST_USER>()
            //                  where item.U_SDG_TYPE == sdgType && item.U_ORGANS.Contains(All_organs)
            //                                     select item).ToList();
            //}
            //else
            //{

            //}
            
            List<U_NLIST_USER> list4Show = new List<U_NLIST_USER>();
            //foreach (var item in organsList)
            //{
            //    foreach (var org in organs4Sdg)
            //    {
            //        if (item.U_ORGANS != null && item.U_ORGANS.Contains(org))
            //        {
            //            list4Show.Add(item);
            //        }
            //    }
            //}
            IEnumerable<string> organs4Show = organsList.Select(SM => SM.U_TEXT).ToList().Distinct();
            if (organs4Show.Count() < 1)
            {
                return null;
            }

            return organs4Show;
        }
    }
}
